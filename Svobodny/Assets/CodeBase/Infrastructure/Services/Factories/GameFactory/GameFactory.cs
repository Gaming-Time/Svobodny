using System.Collections.Generic;
using Cinemachine;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic.Enemies;
using CodeBase.Infrastructure.Logic.Npcs;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticData.Character;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using CodeBase.Modules.Character;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.FOV;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly INpcFactory _npcFactory;
        private readonly IInputService _inputService;

        private Dictionary<string, EnemySpawner> _enemySpawners = new();
        private Dictionary<string, NpcSpawner> _npcSpawners = new();

        public GameFactory(IAssets assetProvider, IEnemyFactory enemyFactory, INpcFactory npcFactory,
            IInputService inputService)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _npcFactory = npcFactory;
            _inputService = inputService;
        }

        public GameObject CreateCharacter(Vector3 position, Quaternion rotation, CharacterStaticData staticData)
        {
            var character = _assetProvider.Instantiate(AssetPath.CharacterPath, position, rotation);
            var camera = Object.FindObjectOfType<Camera>();
            InitMovement(staticData, character);
            InitAnimations(staticData, character);
            InitTransparency(character, camera);
            InitFov(character, camera, _inputService);

            return character;
        }

        private void InitFov(GameObject character, Camera camera, IInputService inputService)
        {
            var moveWithMouseScript = character.GetComponentInChildren<RotateWithMouse>();
            moveWithMouseScript.Construct(camera, inputService);
        }

        public void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID)
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawnerPath, position, rotation)
                .GetComponent<EnemySpawner>();
            spawner.Construct(_enemyFactory);
            _enemySpawners.Add(spawnerID, spawner);
        }

        public void CreateNpcSpawner(Vector3 position, Quaternion rotation, string spawnerId, NpcTypeId typeID)
        {
            NpcSpawner spawner = _assetProvider.Instantiate(AssetPath.NpcSpawnerPath, position, rotation)
                .GetComponent<NpcSpawner>();
            spawner.Construct(_npcFactory);
            _npcSpawners.Add(spawnerId, spawner);
        }

        public void InitCamera(GameObject character)
        {
            var camera = Object.FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = character.transform;
        }

        public void SpawnAllMonsters()
        {
            foreach (var spawner in _enemySpawners)
            {
                spawner.Value.Spawn();
            }
        }

        public void SpawnAllNpcs()
        {
            foreach (var spawner in _npcSpawners)
            {
                spawner.Value.Spawn();
            }
        }

        private static void InitTransparency(GameObject character, Camera camera) =>
            character.GetComponent<PlayerTransparency>().Construct(camera);

        private void InitAnimations(CharacterStaticData staticData, GameObject character)
        {
            var characterAnimationController = character.GetComponent<CharacterAnimatorController>();
            characterAnimationController.Contsruct(_inputService, character.GetComponent<Animator>(),
                staticData.WalkSpeed, staticData.SneakSpeed);
        }

        private void InitMovement(CharacterStaticData staticData, GameObject character)
        {
            var characterMove = character.GetComponent<CharacterMove>();
            characterMove.Construct(_inputService, character.GetComponent<CharacterController>());
            characterMove.Init(staticData.WalkSpeed, staticData.SneakSpeed);
        }
    }
}