using CodeBase.Data.StaticData.Level;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IStaticDataService staticDataService, IProgressService progressService,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            InitUI();
            InitLevel();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitLevel()
        {
            _levelStaticData = LevelStaticData();
            var characterData = _staticDataService.ForCharacter();
            Vector3 playerPosition = new();
            Quaternion playerRotation = new();

            switch (_levelStaticData.initialPhase)
            {
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
            
            CreateObjectSpawners();
            CreateGunObjectsSpawners();
            _gameFactory.CreateHud();
            _gameFactory.CreateHealthUIHandler();
            
            var character = _gameFactory.CreateCharacter(playerPosition, playerRotation, characterData);
            
            _gameFactory.CreateItemsUIHandler();
            _gameFactory.CreateGunsUiHandler();
            
            _gameFactory.InitCamera(character);
            
            _gameFactory.SpawnAllObjects();
            _gameFactory.SpawnGuns();
            _gameFactory.InitTriggers();
        }

        private void InitUI()
        {
            _uiFactory.CreateRoot();
        }

        private void CreateObjectSpawners()
        {
            var spawners = _levelStaticData.ObjectsSpawners;

            foreach (var spawner in spawners)
            {
                _gameFactory.CreateObjectSpawner(spawner.Position, spawner.Rotation, spawner.Id, spawner.TypeId);
            }
        }

        private void CreateGunObjectsSpawners()
        {
            var spawners = _levelStaticData.GunsSpawners;

            spawners.ForEach(spawner =>
                _gameFactory.CreateGunObjectSpawner(spawner.Position, spawner.Rotation, spawner.Id, spawner.GunType));
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
            foreach (var spawner in spawners)
            {
                _gameFactory.CreateNpcSpawner(spawner.Position, spawner.Rotation, spawner.Id, spawner.TypeId);
            }
        }

        private LevelStaticData LevelStaticData() => _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

        public void Exit()
        {
            
        }
    }
}