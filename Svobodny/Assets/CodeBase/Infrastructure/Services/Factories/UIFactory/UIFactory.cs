using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ButtonMediator;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.UI.ButtonHandler;
using CodeBase.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assetProvider;
        private readonly IButtonMediator _buttonMediator;

        private GameObject _uiRoot;
        private Transform _canvas;

        public UIFactory(IAssets assetProvider, IButtonMediator buttonMediator)
        {
            _assetProvider = assetProvider;
            _buttonMediator = buttonMediator;
        }

        public void CreateRoot()
        {
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIPath.UIRoot);
            _canvas = _uiRoot.GetComponentInChildren<Canvas>().transform;
        }

        public GameObject CreateItemsInventory()
        {
            var inventory = _assetProvider.Instantiate(AssetPath.UIPath.ItemsInventory, _canvas);
            return inventory;
        }

        public WindowBase CreateDeathWindow()
        {
            var window = _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DeathMenu, _canvas);

            InitializeButtons(window.gameObject);

            return window;
        }

        public WindowBase CreateWindow(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Death:
                    var window = _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DeathMenu, _canvas);
                    InitializeButtons(window.gameObject);

                    return window;
                case WindowID.DoorOneWindow:
                    return _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DoorOneWindow, _canvas);
                case WindowID.DoorTwoWindow:
                    return _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DoorTwoWindow, _canvas);
                case WindowID.DoorThreeWindow:
                    return _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DoorThreeWindow, _canvas);
                case WindowID.Count:
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowID), windowID, null);
            }
        }

        private void InitializeButtons(GameObject windowGameObject)
        {
            foreach (var buttonHandler in windowGameObject.GetComponentsInChildren<ButtonHandler>())
            {
                buttonHandler.Construct(_buttonMediator);
            }
        }
    }
}