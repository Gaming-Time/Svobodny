using CodeBase.Infrastructure.Services.ButtonMediator;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IButtonMediator _buttonMediator;
        private readonly IWindowService _windowService;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        public Mediator(IButtonMediator buttonMediator, IWindowService windowService, SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _buttonMediator = buttonMediator;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;

            _buttonMediator.RestartEvent += RestartLevel;
            _buttonMediator.ExitToMenuEvent += ExitToMenu;
        }

        private void ExitToMenu()
        {
            Time.timeScale = 1f;
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void RestartLevel()
        {
            Time.timeScale = 1f;
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}