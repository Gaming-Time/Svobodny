using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private Dictionary<LevelType, string> _levels;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;

            _levels = new Dictionary<LevelType, string>
            {
                { LevelType.MainMenu, "MainMenu" },
                { LevelType.Level1, "Level1 1" },
            };
        }

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        public void Load(LevelType levelType, Action onLoaded = null)
        {
            if(!_levels.TryGetValue(levelType, out var levelName))
                return;

            _coroutineRunner.StartCoroutine(LoadScene(levelName, onLoaded));
        }

        public Level CurrentLevel()
        {
            var scene = SceneManager.GetActiveScene();
            var level = new Level
            {
                Index = scene.buildIndex,
                Name = scene.name
            };

            return level;
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }

    public enum LevelType
    {
        MainMenu,
        Level1
    }
}