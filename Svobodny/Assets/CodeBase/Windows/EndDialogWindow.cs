using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Mediator;
using UnityEngine;

namespace CodeBase.Windows
{
    public class EndDialogWindow : DialogWindow
    {
        private IMediator _mediator;

        [SerializeField] private Levels transferTo;

        private void Start()
        {
            _mediator = AllServices.Container.Single<IMediator>();
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