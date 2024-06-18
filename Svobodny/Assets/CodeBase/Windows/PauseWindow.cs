using CodeBase.Data.StaticData.Cursor;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Cursor;
using CodeBase.Infrastructure.Services.Mediator;
using UnityEngine;

namespace CodeBase.Windows
{
    public class PauseWindow : WindowBase
    {
        private IMediator _mediator;
        private ICursorService _cursorService;

        private void Awake()
        {
            _mediator = AllServices.Container.Single<IMediator>();
            _cursorService = AllServices.Container.Single<ICursorService>();
        }

        public override void Activate()
        {
            base.Activate();

            _cursorService.ChangeCursor(CursorType.Menu);
            Time.timeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();

            _cursorService.ChangeCursor(CursorType.Game);
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