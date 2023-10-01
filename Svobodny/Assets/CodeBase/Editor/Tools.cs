using CodeBase.Data;
using UnityEditor;
using UnityEngine;

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
            var progress = new PlayerProgress();
            
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(progress));
            PlayerPrefs.Save();
        }
    }
}