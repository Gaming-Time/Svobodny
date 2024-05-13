namespace CodeBase.Infrastructure.Services.Mediator
{
    public interface IMediator : IService
    {
        void LoadLevel(string levelName);
    }
}