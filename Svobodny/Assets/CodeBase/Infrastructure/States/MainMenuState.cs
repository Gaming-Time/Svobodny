using CodeBase.Modules.UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuSceneName = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private readonly LoadingCurtain _curtain;
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(LoadingCurtain curtain, SceneLoader sceneLoader, GameStateMachine stateMachine)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _curtain.Show();
            if (SceneManager.GetActiveScene().name == MainMenuSceneName)
            {
                _curtain.Hide();
                Object.FindObjectOfType<MenuController>().Construct(_stateMachine);
                return;
            }
            
            _sceneLoader.Load(MainMenuSceneName, () =>
            {
                Object.FindObjectOfType<MenuController>().Construct(_stateMachine);
                _curtain.Hide();
            });
        }
    }
}