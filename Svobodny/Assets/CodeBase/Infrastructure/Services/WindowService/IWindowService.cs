namespace CodeBase.Infrastructure.Services.WindowService
{
    public interface IWindowService : IService
    {
        void OpenOrCreateWindow(WindowID windowID);
        void CloseWindow(WindowID windowID);
        void Cleanup();
    }
}