using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Mediator;
using UnityEngine;

namespace CodeBase.Windows
{
    public class PauseWindow : WindowBase
    {
        private IMediator _mediator;

        private void Awake()
        {
            _mediator = AllServices.Container.Single<IMediator>();
        }

        public override void Activate()
        {
            base.Activate();

            Time.timeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();

            Time.timeScale = 1f;
        }

        public void OnContinueButtonDown()
        {
            Hide();
        }

        public void OnExitButtonDown()
        {
            Time.timeScale = 1f;
            
            _mediator.ExitToMenu();
        }
    }
}