using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateRoot();
        GameObject CreateHud();
        WindowBase CreateWindow(WindowID windowID);
    }
}