using System;
using System.Collections.Generic;
using System.Text;


namespace MauiAppDemo.Services
{
    public interface IAppPreferenceServices
    {
        string Theme { get; }
        int FontSize {get;}

        bool IsDark { get; }

        event Action? OnChange;

        void Load();

        void SetTheme(bool  dark);
        void SetFontSize(int size);
    }
}
