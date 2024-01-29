using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.StaticData.Level;
using System;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, 
            IGameFactory gameFactory, IStaticDataService staticDataService, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            InitLevel();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitLevel()
        {
            _levelStaticData = _staticDataService.ForLevel(_progressService.Progress.LevelName);
            var characterData = _staticDataService.ForCharacter();
            Vector3 playerPosition = new();
            Quaternion playerRotation = new();



            switch (_levelStaticData.initialPhase) {
                case LevelPhase.Day:
                    CreateNpcSpawners();
                    _gameFactory.SpawnAllNpcs();
                    playerPosition = _levelStaticData.DayPlayerPosition;
                    playerRotation = _levelStaticData.DayPlayerRotation;
                    break;
                case LevelPhase.Night:
                    CreateEnemySpawners();
                    _gameFactory.SpawnAllMonsters();
                    playerPosition = _levelStaticData.NightPlayerPosition;
                    playerRotation = _levelStaticData.NightPlayerRotation;
                    break;
            }

            var character = _gameFactory.CreateCharacter(playerPosition, playerRotation, characterData);
            _gameFactory.InitCamera(character);

            CreateObjectSpawners();
            _gameFactory.SpawnAllObjects();
        }

        private void CreateObjectSpawners()
        {
            var spawners = _levelStaticData.ObjectsSpawners;

            foreach (var spawner in spawners)
            {
                _gameFactory.CreateObjectSpawner(spawner.Position, spawner.Id, spawner.TypeId);
            }
        }

        private void CreateEnemySpawners()
        {
            var spawners = _levelStaticData.EnemySpawners;
            foreach (var spawner in spawners)
            {
                _gameFactory.CreateEnemySpawner(spawner.Position, spawner.Rotation, spawner.Id, spawner.TypeId);
            }
        }

        private void CreateNpcSpawners()
        {
            var spawners = _levelStaticData.NpcSpawners;
            foreach(var spawner in spawners)
            {
                _gameFactory.CreateNpcSpawner(spawner.Position, spawner.Rotation, spawner.Id, spawner.TypeId);
            }
        }

        public void Exit() => _loadingCurtain.Hide();
    }
}