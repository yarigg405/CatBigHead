using System;
using System.Collections.Generic;


namespace Yrr.UI
{
    internal sealed class ScreenStorage
    {
        private readonly Dictionary<Type, UIScreen> _screens;

        internal ScreenStorage()
        {
            _screens = new Dictionary<Type, UIScreen>();
        }

        internal void AddScreen(Type key, UIScreen value)
        {
            _screens.Add(key, value);
        }

        internal UIScreen GetScreen(Type key)
        {
            var screen = _screens[key];
            return screen;
        }

    }
}
