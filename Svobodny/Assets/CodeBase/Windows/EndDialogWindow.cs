using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Mediator;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;

namespace CodeBase.Windows
{
    public class EndDialogWindow : DialogWindow
    {
        private IMediator _mediator;
        private ISaveLoadService _saveLoadService;
        private IWindowService _windowService;

        [SerializeField] private Levels transferTo;

        private void Awake()
        {
            _mediator = AllServices.Container.Single<IMediator>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _windowService = AllServices.Container.Single<IWindowService>();
        }

        public override void Activate()
        {
            base.Activate();
            
            _saveLoadService.SaveProgress();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
            if(transferTo == Levels.None)
            {
                _windowService.OpenOrCreateWindow(WindowID.FinalDialog);
                return;
            }

            var levelName = transferTo switch
            {
                Levels.Level1 => LevelNames.Level1,
                Levels.Level2 => LevelNames.Level2,
                Levels.Level3 => LevelNames.Level3,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _mediator.LoadLevel(levelName);
        }
    }
}