using System.Collections;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;
        private readonly LoadingCurtain _curtain;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IProgressService _progressService;

        private bool _isCurtainFadeOut;

        public GameLoopState(IGameFactory gameFactory, IWindowService windowService, ICoroutineRunner coroutineRunner,
            LoadingCurtain curtain, IProgressService progressService)
        {
            _gameFactory = gameFactory;
            _windowService = windowService;
            _coroutineRunner = coroutineRunner;
            _curtain = curtain;
            _progressService = progressService;
        }

        public void Enter()
        {
            _gameFactory.PlayGameMusic();
            Time.timeScale = 1;
            _curtain.Hide();
            _coroutineRunner.StartCoroutine(WaitForCurtainToFadeOut());
        }

        public void Exit()
        {
            _gameFactory.Cleanup();
            _windowService.Cleanup();
        }

        private IEnumerator WaitForCurtainToFadeOut()
        {
            yield return new WaitUntil(() => _curtain.IsHidden);
            ShowStartDialog();
        }

        private void ShowStartDialog()
        {
            var currentLevel = SceneManager.GetActiveScene().name;
            bool hasDialogBeenShowedPreviously;
            WindowID windowId;
            switch (currentLevel)
            {
                case LevelNames.Level1:
                    windowId = WindowID.Level1InitialDialog;
                    hasDialogBeenShowedPreviously = _progressService.Progress.StartDialogsState.FirstDialogDone;
                    break;
                case LevelNames.Level2:
                    windowId = WindowID.Level2InitialDialog;
                    hasDialogBeenShowedPreviously = _progressService.Progress.StartDialogsState.SecondDialogDone;
                    break;
                case LevelNames.Level3:
                    windowId = WindowID.Level3InitialDialog;
                    hasDialogBeenShowedPreviously = _progressService.Progress.StartDialogsState.ThirdDialogDone;
                    break;
                default:
                    return;
            }
            
            if(hasDialogBeenShowedPreviously)
                return;

            _windowService.OpenOrCreateWindow(windowId);
        }
    }
}