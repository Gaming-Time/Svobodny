using CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}