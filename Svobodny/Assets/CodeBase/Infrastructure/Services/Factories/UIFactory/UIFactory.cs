using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assetProvider;

        private GameObject _uiRoot;
        private Transform _canvas;

        public UIFactory(IAssets assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateRoot()
        {
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIPath.UIRoot);
            _canvas = _uiRoot.GetComponentInChildren<Canvas>().transform;
        }

        public WindowBase CreateDeathWindow()
        {
            return _assetProvider.Instantiate<WindowBase>(AssetPath.UIPath.DeathMenu, _canvas);
        }
    }
}