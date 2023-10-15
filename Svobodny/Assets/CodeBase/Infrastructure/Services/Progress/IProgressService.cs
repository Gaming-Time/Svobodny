using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}
