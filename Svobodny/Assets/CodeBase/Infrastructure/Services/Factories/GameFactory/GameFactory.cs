using System.Collections.Generic;
using Cinemachine;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic.Enemies;
using CodeBase.Infrastructure.Logic.Npcs;
using CodeBase.Infrastructure.Logic.UsableObjects;
using CodeBase.Infrastructure.Logic.UsableObjects.Closet;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.StaticData.Character;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using CodeBase.Modules.Character;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.FOV;
using CodeBase.Modules.Character.Health;
using CodeBase.Modules.Character.Interaction;
using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Ai;
using CodeBase.Modules.Enemies.Ai.Entity;
using CodeBase.Modules.Enemies.Attack;
using CodeBase.Modules.Enemies.Health;
using CodeBase.Modules.Enemies.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly INpcFactory _npcFactory;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;
        private readonly IUsableObjectFactory _usableObjectFactory;

        private Dictionary<string, EnemySpawner> _enemySpawners = new();
        private Dictionary<string, NpcSpawner> _npcSpawners = new();
        private Dictionary<string, UsableObjectSpawner> _objectSpawners = new();

        private GameObject _character;

        public GameFactory(IAssets assetProvider, IEnemyFactory enemyFactory, INpcFactory npcFactory,
            IInputService inputService, IStaticDataService staticData, IUsableObjectFactory usableObjectFactory)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _npcFactory = npcFactory;
            _inputService = inputService;
            _staticData = staticData;
            _usableObjectFactory = usableObjectFactory;
        }

        public GameObject CreateCharacter(Vector3 position, Quaternion rotation, CharacterStaticData staticData)
        {
            _character = _assetProvider.Instantiate(AssetPath.CharacterPath, position, rotation);
            var camera = Object.FindObjectOfType<Camera>();
            InitMovement(staticData, _character);
            InitAnimations(staticData, _character);
            InitTransparency(_character, camera);
            InitFov(_character, camera, _inputService);
            InitHealth(staticData, _character);
            InitInteractions(_character);

            return _character;
        }

        private void InitInteractions(GameObject character)
        {
            var wardrobeInteraction = _character.GetComponent<CharacterWardrobeInteraction>();
            var animatorController = character.GetComponent<CharacterAnimatorController>();
            var characterController = character.GetComponent<CharacterController>();
            var characterMove = character.GetComponent<CharacterMove>();

            wardrobeInteraction.Construct(animatorController, characterController, characterMove);
        }

        private void InitHealth(CharacterStaticData staticData, GameObject character)
        {
            var characterHealth = character.GetComponent<CharacterHealth>();
            characterHealth.Construct(staticData.Health);
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
            spawner.Construct(_enemyFactory, typeID);
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

        public void CreateObjectSpawner(Vector3 spawnerPosition, string spawnerId, UsableObjectTypeId spawnerTypeId)
        {
            var spawner = _assetProvider.Instantiate(AssetPath.ObjectSpawnerPath, spawnerPosition)
                .GetComponent<UsableObjectSpawner>();
            spawner.Construct(_usableObjectFactory, spawnerTypeId);
            _objectSpawners.Add(spawnerId, spawner);
        }

        public void SpawnAllObjects()
        {
            foreach (var spawner in _objectSpawners)
            {
                var usableObject = spawner.Value.Spawn();

                switch (spawner.Value.TypeId)
                {
                    case UsableObjectTypeId.Wardrobe:
                        var wardrobeAnimatorController = usableObject.GetComponent<WardrobeAnimatorController>();
                        wardrobeAnimatorController.Construct(usableObject.GetComponentInChildren<Animator>());

                        var characterWardrobeInteraction = _character.GetComponent<CharacterWardrobeInteraction>();

                        var wardrobe = usableObject.GetComponent<Wardrobe>();
                        wardrobe.Construct(_inputService, _character,
                            wardrobeAnimatorController, characterWardrobeInteraction);

                        var wardrobeAnimationEventsManager =
                            usableObject.GetComponentInChildren<WardrobeAnimationEventsManager>();
                        wardrobeAnimationEventsManager.Construct(wardrobe);

                        break;
                }
            }
        }

        public void SpawnAllMonsters()
        {
            foreach (var spawner in _enemySpawners)
            {
                var monster = spawner.Value.Spawn();
                var monsterType = spawner.Value.TypeID;
                var monsterData = _staticData.ForMonster(monsterType);

                var monsterAgent = monster.GetComponent<NavMeshAgent>();
                var monsterMover = monster.GetComponent<HumanoidMove>();
                var monsterHealth = monster.GetComponent<EnemyHealth>();
                var monsterEntity = monster.GetComponentInChildren<EnemyAiEntity>();
                var monsterContextProvider = monster.GetComponentInChildren<EnemyContextProvider>();
                var monsterAnimatorController = monster.GetComponentInChildren<HumanoidAnimatorController>();
                var collisionOwner = monster.GetComponentInChildren<CollisionOwner>();
                var monsterAttack = monster.GetComponent<EnemyAttack>();


                monsterMover.Construct(monsterAgent, monsterData.Speed);
                monsterHealth.Construct(monsterData.Health);
                monsterAnimatorController.Construct(monster.GetComponentInChildren<Animator>(), monsterMover);
                monsterEntity.Construct(monsterMover, monsterData.ScanRange, monsterData.MeleeAttackRange);
                monsterContextProvider.Construct(monsterEntity, spawner.Value.transform.position);
                collisionOwner.Construct(monsterEntity);
                monsterAttack.Construct(monsterData.MeleeAttackRange, monsterAnimatorController);
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
            characterAnimationController.Construct(_inputService, character.GetComponent<Animator>(),
                character.GetComponent<CharacterController>(), staticData.WalkSpeed, staticData.SneakSpeed);
        }

        private void InitMovement(CharacterStaticData staticData, GameObject character)
        {
            var characterMove = character.GetComponent<CharacterMove>();
            characterMove.Construct(_inputService, character.GetComponent<CharacterController>());
            characterMove.Init(staticData.WalkSpeed, staticData.SneakSpeed);
        }
    }
}