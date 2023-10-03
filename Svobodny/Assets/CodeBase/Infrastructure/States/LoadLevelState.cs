using Assets.CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.StaticData.Level;
using System;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, 
            IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
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
            _levelStaticData = _staticDataService.ForLevel("Level1");

            switch (_levelStaticData.initialPhase) {
                case LevelPhase.Day:
                    CreateNpcSpawners();
                    _gameFactory.CreateCharacter(_levelStaticData.DayPlayerPosition, _levelStaticData.DayPlayerRotation);
                    _gameFactory.SpawnAllNpcs();
                    break;
                case LevelPhase.Night:
                    CreateEnemySpawners();
                    _gameFactory.CreateCharacter(_levelStaticData.NightPlayerPosition, _levelStaticData.NightPlayerRotation);
                    _gameFactory.SpawnAllMonsters();
                    break;
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