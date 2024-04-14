using CodeBase.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateRoot();
        WindowBase CreateDeathWindow();
        GameObject CreateItemsInventory();
    }
}