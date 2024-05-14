using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Windows;
using UnityEngine;

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
            var window = _uiFactory.CreateWindow(windowID);
            _windows.TryAdd(windowID, window);

            return window;
        }

        private void OpenWindow(WindowBase window) => window.Activate();

        public void Cleanup() => _windows.Clear();
    }

    public enum WindowID
    {
        Death,
        DoorOneWindow,
        DoorTwoWindow,
        DoorThreeWindow,
        DoorFourWindow,
        DoorFiveWindow,
        DoorSixWindow,
        Level1InitialDialog,
        Level2InitialDialog,
        Level3InitialDialog,
        Level1EndDialog,
        Level2EndDialog,
        Level3EndDialog,
        KnifeDialog,
        Count
    }
}