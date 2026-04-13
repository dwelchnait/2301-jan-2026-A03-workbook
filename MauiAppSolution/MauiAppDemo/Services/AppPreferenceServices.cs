using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppDemo.Services
{
    public class AppPreferenceServices : IAppPreferenceServices
    {
        //keys values for your preference can be any character string
        //recommended is a character string with meaning to the preference
        private const string ThemeKey = "app_theme";
        private const string FontSizeKey = "app-fontsize";

        public event Action? OnChange; //interface

        //using a private set means that the property can only be given
        //  a value from within the class itself
        //setting of the property will ONLY be allow via constructor or class method
        //  by the outside user
        public string Theme { get; private set; } = "light"; //default, interface
        public int FontSize { get; private set; } = 16;


        public bool IsDark => Theme == "dark"; //interface

        public void Load() //interface
        {
            //get the current setting for a preference
            //preference are kept with the application is a dictionary style storage area
            //retreive the preference using the appropriate key value

            //parameters are 1) the dictionary key value, 2) default if key not found
            Theme = Preferences.Get(ThemeKey, "light");
            FontSize = Preferences.Get(FontSizeKey, 16);
        }

        public void SetTheme(bool dark) // interface
        {
            //light or dark
            //ternary operator:  condition(s) ? true value : false value
            Theme = dark ? "dark" : "light";

            //set the app preference dictionary entry
            Preferences.Set(ThemeKey, Theme);

            //signal the system of a change
            Notify();
        }

        public void SetFontSize(int size) // interface
        {
            if (size < 8)
                size = 8;
            
            //ternary operator:  condition(s) ? true value : false value
            FontSize = size;

            //set the app preference dictionary entry
            Preferences.Set(FontSizeKey, FontSize);

            //signal the system of a change
            Notify();
        }

        //signal system of a change state
        //it will invoke an event that sends a signal to the system
        //  to do something
        private void Notify() => OnChange?.Invoke();
    }
}
