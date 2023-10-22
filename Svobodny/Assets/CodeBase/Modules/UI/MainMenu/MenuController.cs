using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Modules.UI.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Levels to load")] 
        [SerializeField]
        private string newGameLevel;

        private string _levelToLoad;

        public void NewGameDialogYes()
        {
            SceneManager.LoadScene(newGameLevel);
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