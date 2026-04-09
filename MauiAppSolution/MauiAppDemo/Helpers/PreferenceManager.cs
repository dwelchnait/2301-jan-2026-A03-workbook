using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppDemo.Helpers
{
    public static class PreferenceManager
    {
        public static event Action? OnPreferenceChanged;

        public static void NotifyPreferenceChanged()
        {
            OnPreferenceChanged?.Invoke();
        }
    }
}
