using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Mediator;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Windows
{
    public class FinalDialogWindow : WindowBase
    {
        private GameStateMachine _stateMachine;
        private IMediator _mediator;

        [SerializeField] private GameObject[] frames;

        private int _currentFrameIndex;

        private void Awake()
        {
            _mediator = AllServices.Container.Single<IMediator>();
        }

        public override void Activate()
        {
            base.Activate();

            _currentFrameIndex = 0;
            frames[_currentFrameIndex].SetActive(true);
        }

        private void Update()
        {
            if (Input.anyKeyDown)
                ShowNextFrame();
        }

        private void ShowNextFrame()
        {
            frames[_currentFrameIndex].SetActive(false);

            _currentFrameIndex++;
            
            if(_currentFrameIndex >= frames.Length)
            {
                PlayerPrefs.DeleteKey("Progress");
                _mediator.ExitToMenu();
                return;
            }
            
            frames[_currentFrameIndex].SetActive(true);
        }
    }
}