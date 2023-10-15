using CodeBase.Data;
using CodeBase.Infrastructure.Services.StaticData;
using System;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;

        public LoadProgressState(GameStateMachine gameStateMachine, IStaticDataService staticDataService, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void Exit()
        {
           
        }

        public void Enter()
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.LevelName);
        }

        private void LoadProgress()
        {
            _progressService.Progress = PlayerPrefs.GetString("Progress")?.ToDeserialized<PlayerProgress>() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress("Level1");

            return progress;
        }
    }
}