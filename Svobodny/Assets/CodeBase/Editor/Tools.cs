using CodeBase.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        [MenuItem("Tools/ Start from current scene")]
        public static void CurrentLevel()
        {
            const string ProgressKey = "Progress";
            PlayerPrefs.DeleteKey(ProgressKey);
            var progress = new PlayerProgress(SceneManager.GetActiveScene().name);
            
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(progress));
            PlayerPrefs.Save();
        }
    }
}