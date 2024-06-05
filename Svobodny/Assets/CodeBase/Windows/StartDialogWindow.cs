using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Windows
{
    public class StartDialogWindow : DialogWindow
    {
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public override void Activate()
        {
            base.Activate();
            
            _saveLoadService.SaveProgress();
        }
    }
}