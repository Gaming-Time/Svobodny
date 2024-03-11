using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Windows;

namespace CodeBase.Infrastructure.Services.WindowService
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        private Dictionary<WindowID, WindowBase> _windows = new();

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void OpenOrCreateWindow(WindowID windowID)
        {
            if (!_windows.TryGetValue(windowID, out var window))
                window = CreateWindow(windowID);

            OpenWindow(window);
        }

        public void CloseWindow(WindowID windowID)
        {
            if(_windows.TryGetValue(windowID, out var window))
                window.Hide();
        }

        private WindowBase CreateWindow(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Death:
                    var deathWindow = _uiFactory.CreateDeathWindow();
                    _windows.TryAdd(windowID, deathWindow);

                    return deathWindow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowID), windowID,
                        $"{windowID} не определён в фабрике");
            }
        }

        private void OpenWindow(WindowBase window) => window.Activate();
    }

    public enum WindowID
    {
        Death,
        Count
    }
}