using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Modules.UI.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Levels to load")] 
        [SerializeField]
        private string newGameLevel;

        private GameStateMachine _gameStateMachine;
        private string _levelToLoad;

        public void Construct(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        public void NewGameDialogYes()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }

        public void LoadGameDialogYes()
        {
        }

        public void ExitButton()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}