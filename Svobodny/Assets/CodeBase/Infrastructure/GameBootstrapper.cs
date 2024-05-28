using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private const string MenuSceneName = "MainMenu";
        
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(this,Instantiate(CurtainPrefab));
            
            if(SceneManager.GetActiveScene().name != MenuSceneName)
                _game.StateMachine.Enter<BootstrapState>();
            else
            {
                _game.StateMachine.Enter<MainMenuState>();
            }
            
            DontDestroyOnLoad(this);
        }
    }
}