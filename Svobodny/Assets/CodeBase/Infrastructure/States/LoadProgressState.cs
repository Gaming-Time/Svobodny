using CodeBase.Data;
using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Modules.Character.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string DefaultLevelName = "Level1 1";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;

        private Vector3 _initialPlayerPosition;

        public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            //_initialPlayerPosition = _staticDataService.ForLevel(DefaultLevelName).NightPlayerPosition;
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.PositionOnLevel.Level);
        }

        private void LoadProgress()
        {
            _progressService.Progress =
                _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress(DefaultLevelName);

            return progress;
        }
    }
}