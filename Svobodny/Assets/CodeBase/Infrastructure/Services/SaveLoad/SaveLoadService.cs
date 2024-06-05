using CodeBase.Data;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }


        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }

            var currentScene = SceneManager.GetActiveScene().name;

            switch (currentScene)
            {
                case LevelNames.Level1:
                    _progressService.Progress.StartDialogsState.FirstDialogDone = true;
                    break;
                case LevelNames.Level2:
                    _progressService.Progress.StartDialogsState.SecondDialogDone = true;
                    break;
                case LevelNames.Level3:
                    _progressService.Progress.StartDialogsState.ThirdDialogDone = true;
                    break;
            }

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}