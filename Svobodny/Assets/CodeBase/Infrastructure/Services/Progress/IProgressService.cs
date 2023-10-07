using CodeBase.Data;
using CodeBase.Infrastructure.Services;

namespace Assets.CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}
