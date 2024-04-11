using CodeBase.Windows;

namespace CodeBase.Infrastructure.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateRoot();
        WindowBase CreateDeathWindow();
    }
}