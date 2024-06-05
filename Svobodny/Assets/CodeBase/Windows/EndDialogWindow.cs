using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Mediator;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Windows
{
    public class EndDialogWindow : DialogWindow
    {
        private IMediator _mediator;
        private ISaveLoadService _saveLoadService;

        [SerializeField] private Levels transferTo;

        private void Awake()
        {
            _mediator = AllServices.Container.Single<IMediator>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
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
                return;

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