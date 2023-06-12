using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AaronMorris_C971_Task1.Services
{
    public static class Settings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }
}
