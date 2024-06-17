using System;
using System.Collections.Generic;
using Cinemachine;
using CodeBase.Data.StaticData.Character;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Logic.Enemies;
using CodeBase.Logic.Npcs;
using CodeBase.Logic.Triggers;
using CodeBase.Logic.UsableObjects;
using CodeBase.Logic.UsableObjects.Closet;
using CodeBase.Logic.UsableObjects.Doors;
using CodeBase.Logic.UsableObjects.Essentials;
using CodeBase.Logic.UsableObjects.Key;
using CodeBase.Modules.Character;
using CodeBase.Modules.Character.Ai;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Arm;
using CodeBase.Modules.Character.Attack;
using CodeBase.Modules.Character.Audio;
using CodeBase.Modules.Character.FOV;
using CodeBase.Modules.Character.Health;
using CodeBase.Modules.Character.Interaction;
using CodeBase.Modules.Character.StateMachine;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Character.VFX;
using CodeBase.Modules.Enemies.Ai;
using CodeBase.Modules.Enemies.Ai.Entity;
using CodeBase.Modules.Enemies.Animation;
using CodeBase.Modules.Enemies.Attack;
using CodeBase.Modules.Enemies.Audio;
using CodeBase.Modules.Enemies.Health;
using CodeBase.Modules.Enemies.Interactions;
using CodeBase.Modules.Enemies.Movement;
using CodeBase.Modules.Enemies.VFX;
using CodeBase.Modules.Health;
using CodeBase.Modules.Inventory;
using CodeBase.Modules.Inventory.Essentials;
using CodeBase.Modules.Inventory.Guns;
using CodeBase.Modules.Music;
using CodeBase.Modules.UI;
using UnityEngine;
using UnityEngine.AI;
using Essential = CodeBase.Logic.UsableObjects.Essentials.Essential;
using Gun = CodeBase.Logic.UsableObjects.Gun;
using Object = UnityEngine.Object;

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
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;
        private readonly IEnemyDetectionService _enemyDetectionService;

        private Dictionary<string, EnemySpawner> _enemySpawners = new();
        private Dictionary<string, NpcSpawner> _npcSpawners = new();
        private Dictionary<string, UsableObjectSpawner> _objectSpawners = new();
        private Dictionary<string, GunUsableObjectSpawner> _gunSpawners = new();
        private Dictionary<string, EssentialUsableObjectSpawner> _essentialSpawners = new();

        private GameObject _character;
        private InventoryHandler _inventoryHandler;
        private ItemsUIHandler _itemsUIHandler;
        private GunsUIHandler _gunsUIHandler;
        private Hud _hud;
        private HealthUIHandler _healthUIHandler;
        private GameMusic _gameMusic;
        private EssentialsUIHandler _essentialsUIHandler;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        public GameFactory(IAssets assetProvider, IEnemyFactory enemyFactory, INpcFactory npcFactory,
            IInputService inputService, IStaticDataService staticData, IUsableObjectFactory usableObjectFactory,
            IWindowService windowService, IUIFactory uiFactory, IEnemyDetectionService enemyDetectionService)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _npcFactory = npcFactory;
            _inputService = inputService;
            _staticData = staticData;
            _usableObjectFactory = usableObjectFactory;
            _windowService = windowService;
            _uiFactory = uiFactory;
            _enemyDetectionService = enemyDetectionService;
        }

        public GameObject CreateCharacter(Vector3 position, Quaternion rotation, CharacterStaticData staticData)
        {
            _character = _assetProvider.Instantiate(AssetPath.CharacterPath, position, rotation);
            var camera = Object.FindObjectOfType<Camera>();
            var audioController = _character.GetComponentInChildren<CharacterAudioController>();
            audioController.Construct(_staticData);
            InitMovement(staticData, _character);
            InitAnimations(staticData, _character, camera);
            InitInventoryHandler(_character);
            InitTransparency(_character, camera);
            InitFov(_character, camera, _inputService);
            InitHealth(staticData, _character);
            InitInteractions(_character);
            InitCharacterAttack(_character, audioController);
            _character.GetComponent<CharacterRangeAttack>().Construct(_inputService, _enemyDetectionService,
                _character.GetComponent<CharacterVFXController>(),
                audioController, _inventoryHandler, camera);
            InitStateMachine(_character, camera, audioController);
            InitArm(_character, camera);

            _character.GetComponent<CharacterInputHandler>().Construct(_windowService, _inputService);
            _character.GetComponent<PlayerEntity>().Construct(_inputService);

            return _character;
        }

        public void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID,
            List<Vector3> waypoints)
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawnerPath, position, rotation)
                .GetComponent<EnemySpawner>();
            spawner.Construct(_enemyFactory, typeID,
                waypoints);
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

        public void CreateObjectSpawner(Vector3 spawnerPosition, Quaternion spawnerRotation, string spawnerId,
            UsableObjectTypeId spawnerTypeId)
        {
            var spawner = _assetProvider.Instantiate(AssetPath.ObjectSpawnerPath, spawnerPosition, spawnerRotation)
                .GetComponent<UsableObjectSpawner>();
            spawner.Construct(_usableObjectFactory, spawnerTypeId);
            _objectSpawners.Add(spawnerId, spawner);
        }

        public void CreateGunObjectSpawner(Vector3 spawnerPosition, Quaternion spawnerRotation, string spawnerId,
            GunType gunType)
        {
            var spawner = _assetProvider.Instantiate<GunUsableObjectSpawner>(AssetPath.GunObjectSpawnerPath,
                spawnerPosition, spawnerRotation);
            spawner.Construct(_usableObjectFactory, gunType);
            _gunSpawners.Add(spawnerId, spawner);
        }

        public void CreateEssentialObjectSpawner(Vector3 position, Quaternion rotation, string id,
            EssentialType essentialType, int amount)
        {
            var spawner =
                _assetProvider.Instantiate<EssentialUsableObjectSpawner>(AssetPath.EssentialObjectSpawnerPath, position,
                    rotation);
            spawner.Construct(_usableObjectFactory, essentialType, amount);
            _essentialSpawners.Add(id, spawner);
        }

        public void CreateHud() => _hud = _uiFactory.CreateHud().GetComponent<Hud>();

        public void CreateItemsUIHandler()
        {
            _itemsUIHandler = _assetProvider.Instantiate<ItemsUIHandler>(AssetPath.ItemsUIHandlerPath);
            _itemsUIHandler.Construct(_inventoryHandler, _hud, _staticData, _assetProvider, _uiFactory, _inputService);
        }

        public void CreateGunsUiHandler()
        {
            _gunsUIHandler = _assetProvider.Instantiate<GunsUIHandler>(AssetPath.GunsUIHandlerPath);
            _gunsUIHandler.Construct(_inventoryHandler, _hud, _assetProvider, _staticData);
        }

        public void CreateHealthUIHandler()
        {
            _healthUIHandler = _assetProvider.Instantiate<HealthUIHandler>(AssetPath.HealthUIHandlerPath);
            _healthUIHandler.Construct(_hud);
        }

        public void CreateEssentialsUIHandler()
        {
            _essentialsUIHandler = _assetProvider.Instantiate<EssentialsUIHandler>(AssetPath.EssentialsUIHandlerPath);
            _essentialsUIHandler.Construct(_inventoryHandler, _hud, _assetProvider, _staticData);
        }

        public void SpawnAllObjects()
        {
            foreach (var spawner in _objectSpawners)
            {
                var usableObject = spawner.Value.Spawn();

                InitUsableObject(spawner, usableObject);
            }
        }

        public void SpawnGuns()
        {
            foreach (var gunSpawner in _gunSpawners)
            {
                var gun = gunSpawner.Value.Spawn();
                gun.GetComponent<Gun>().Construct(_inputService, _windowService, _inventoryHandler,
                    gunSpawner.Value.GunType);
            }
        }

        public void SpawnEssentials()
        {
            foreach (var essentialSpawner in _essentialSpawners)
            {
                var essential = essentialSpawner.Value.Spawn();
                essential.GetComponent<Essential>().Construct(_inputService, _inventoryHandler,
                    essentialSpawner.Value.EssentialType,
                    essentialSpawner.Value.Amount);
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
                var animationEventHandler = monster.GetComponentInChildren<HumanoidAnimationEventsHandler>();
                var vfxController = monster.GetComponent<EnemyVFXController>();
                var audioController = monster.GetComponentInChildren<EnemyAudioController>();
                var wardrobeInteraction = monster.GetComponent<EnemyWardrobeInteraction>();

                audioController.Construct(_staticData);
                monsterMover.Construct(monsterAgent, animationEventHandler, audioController, monsterData.Speed);
                monsterHealth.Construct(monsterAnimatorController, animationEventHandler, vfxController,
                    audioController, monsterEntity, monsterData.Health);
                monsterAnimatorController.Construct(monster.GetComponentInChildren<Animator>(), monsterMover);
                wardrobeInteraction.Construct(monsterAnimatorController);
                monsterAttack.Construct(monsterData.MeleeAttackRange, monsterAnimatorController, animationEventHandler,
                    vfxController, audioController);
                monsterEntity.Construct(_enemyDetectionService, monsterMover, monsterAttack, monsterHealth,
                    audioController, wardrobeInteraction,
                    monsterData.ScanRange,
                    monsterData.MeleeAttackRange, spawner.Value.Waypoints);
                monsterContextProvider.Construct(monsterEntity, spawner.Value.transform.position);
                collisionOwner.Construct(monsterEntity);
            }
        }

        public void SpawnAllNpcs()
        {
            foreach (var spawner in _npcSpawners)
            {
                spawner.Value.Spawn();
            }
        }

        public void InitTriggers()
        {
            foreach (var dialogTrigger in Object.FindObjectsOfType<DialogTrigger>())
            {
                dialogTrigger.Construct(_windowService);
            }
        }

        public void Cleanup()
        {
            _enemySpawners.Clear();
            _npcSpawners.Clear();
            _objectSpawners.Clear();
            _gunSpawners.Clear();
            _essentialSpawners.Clear();
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        public void InitGameMusic()
        {
            _gameMusic = Object.FindObjectOfType<GameMusic>();
            _gameMusic.Construct(_staticData);
        }

        public void InitMusicTriggers()
        {
            var triggers = Object.FindObjectsOfType<MusicTrigger>();
            foreach (var musicTrigger in triggers)
            {
                musicTrigger.Construct(_staticData);
            }
        }

        public void PlayGameMusic() => _gameMusic.Play();

        public void InitMeatBlobs()
        {
            var blobs = Object.FindObjectsOfType<MeatBlobSound>();
            foreach (var meatBlobSound in blobs)
            {
                meatBlobSound.Construct(_staticData);
            }
        }

        public void InitSaveTriggers()
        {
            var saveTriggers = Object.FindObjectsOfType<SaveTrigger>();
            var saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            foreach (var saveTrigger in saveTriggers)
            {
                saveTrigger.Construct(saveLoadService);
            }
        }

        private void InitArm(GameObject character, Camera camera)
        {
            character.GetComponentInChildren<ArmAnimatorController>(true)
                .Construct(_inputService, camera, character.transform);
        }

        private void InitStateMachine(GameObject character, Camera camera, CharacterAudioController audioController)
        {
            character.GetComponent<CharacterStateMachine>().Construct(_inputService, _windowService,
                character.GetComponent<CharacterMove>(), character.GetComponent<CharacterMeleeAttack>(),
                character.GetComponent<CharacterAnimationEventsHandler>(), _inventoryHandler,
                character.GetComponent<CharacterRangeAttack>(),
                character.GetComponent<CharacterAnimatorController>(), audioController,
                character.GetComponent<CharacterController>(), character.GetComponent<PlayerEntity>(), camera);
        }

        private void InitInventoryHandler(GameObject character)
        {
            _inventoryHandler = character.GetComponent<InventoryHandler>();
            _inventoryHandler.Construct(_inputService, _windowService,
                character.GetComponent<CharacterAnimatorController>());
            ProgressWriters.Add(_inventoryHandler);
            ProgressReaders.Add(_inventoryHandler);
        }

        private void InitCharacterAttack(GameObject character, CharacterAudioController audioController)
        {
            var characterAttack = character.GetComponent<CharacterMeleeAttack>();
            characterAttack.Construct(character.GetComponent<CharacterAnimatorController>(),
                character.GetComponent<CharacterAnimationEventsHandler>(),
                character.GetComponent<CharacterVFXController>(),
                audioController);
        }

        private void InitInteractions(GameObject character)
        {
            var wardrobeInteraction = _character.GetComponent<CharacterWardrobeInteraction>();
            var animatorController = character.GetComponent<CharacterAnimatorController>();
            var animationEventsHandler = character.GetComponent<CharacterAnimationEventsHandler>();
            var characterController = character.GetComponent<CharacterController>();
            var characterMove = character.GetComponent<CharacterMove>();

            wardrobeInteraction.Construct(_windowService, animatorController, animationEventsHandler,
                characterController, characterMove);
        }

        private void InitHealth(CharacterStaticData staticData, GameObject character)
        {
            var characterHealth = character.GetComponent<CharacterHealth>();
            var healthHandler = character.GetComponent<HealthHandler>();
            var characterAnimationEvents = character.GetComponent<CharacterAnimationEventsHandler>();

            healthHandler.Construct(_healthUIHandler, staticData.Health);
            characterHealth.Construct(_inputService, character.GetComponent<CharacterAnimatorController>(),
                character.GetComponent<CharacterVFXController>(), healthHandler,
                character.GetComponent<CharacterStateMachine>(), _inventoryHandler,
                staticData.Health);

            ProgressReaders.Add(characterHealth);
            ProgressWriters.Add(characterHealth);
        }

        private void InitFov(GameObject character, Camera camera, IInputService inputService)
        {
            var moveWithMouseScript = character.GetComponentInChildren<RotateWithMouse>();
            moveWithMouseScript.Construct(camera, inputService);
        }


        private static void InitTransparency(GameObject character, Camera camera) =>
            character.GetComponent<PlayerTransparency>().Construct(camera);

        private void InitAnimations(CharacterStaticData staticData, GameObject character, Camera camera)
        {
            var characterAnimationController = character.GetComponent<CharacterAnimatorController>();
            characterAnimationController.Construct(_inputService, character.GetComponent<Animator>(),
                character.GetComponent<CharacterController>(), camera, staticData.WalkSpeed, staticData.SneakSpeed);
        }

        private void InitMovement(CharacterStaticData staticData, GameObject character)
        {
            var characterMove = character.GetComponent<CharacterMove>();
            characterMove.Construct(_inputService, character.GetComponent<CharacterController>());
            characterMove.Init(staticData.WalkSpeed, staticData.SneakSpeed);

            ProgressReaders.Add(characterMove);
            ProgressWriters.Add(characterMove);
        }

        private void InitUsableObject(KeyValuePair<string, UsableObjectSpawner> spawner, GameObject usableObject)
        {
            switch (spawner.Value.TypeId)
            {
                case UsableObjectTypeId.Wardrobe:
                    var wardrobeAnimatorController = usableObject.GetComponent<WardrobeAnimatorController>();
                    wardrobeAnimatorController.Construct(usableObject.GetComponentInChildren<Animator>());

                    var characterWardrobeInteraction = _character.GetComponent<CharacterWardrobeInteraction>();

                    var wardrobe = usableObject.GetComponent<Wardrobe>();
                    wardrobe.Construct(_inputService, _enemyDetectionService,
                        wardrobeAnimatorController, characterWardrobeInteraction);

                    var wardrobeAnimationEventsManager =
                        usableObject.GetComponentInChildren<WardrobeAnimationEventsManager>();
                    wardrobeAnimationEventsManager.Construct(wardrobe);

                    break;

                case UsableObjectTypeId.Door:
                    var door = usableObject.GetComponent<Door>();
                    var doorAnimatorController = door.GetComponent<DoorAnimatorController>();
                    var doorAudioController = door.GetComponent<DoorAudioController>();

                    var doorAnimator = door.GetComponent<Animator>();

                    doorAnimatorController.Construct(doorAnimator);
                    doorAudioController.Construct(_staticData);
                    door.Construct(_inputService, doorAnimatorController, doorAudioController);

                    break;

                //Лютый хардкод, нет времени написать нормально
                case UsableObjectTypeId.KeyOne:
                case UsableObjectTypeId.KeyTwo:
                case UsableObjectTypeId.KeyThree:
                case UsableObjectTypeId.KeyFour:
                case UsableObjectTypeId.KeyFive:
                case UsableObjectTypeId.KeySix:
                    var key = usableObject.GetComponent<Key>();
                    var itemType = spawner.Value.TypeId switch
                    {
                        UsableObjectTypeId.KeyOne => ItemType.KeyOne,
                        UsableObjectTypeId.KeyTwo => ItemType.KeyTwo,
                        UsableObjectTypeId.KeyThree => ItemType.KeyThree,
                        UsableObjectTypeId.KeyFour => ItemType.KeyFour,
                        UsableObjectTypeId.KeyFive => ItemType.KeyFive,
                        UsableObjectTypeId.KeySix => ItemType.KeySix,
                    };
                    key.Construct(_inputService, _inventoryHandler, itemType);

                    break;

                case UsableObjectTypeId.ClosedDoorOne:
                case UsableObjectTypeId.ClosedDoorTwo:
                case UsableObjectTypeId.ClosedDoorThree:
                case UsableObjectTypeId.ClosedDoorFour:
                case UsableObjectTypeId.ClosedDoorFive:
                case UsableObjectTypeId.ClosedDoorSix:
                    InitializeClosedDoor(usableObject, spawner.Value.TypeId);

                    break;
                case UsableObjectTypeId.Count:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitializeClosedDoor(GameObject doorObject, UsableObjectTypeId typeId)
        {
            var door = doorObject.GetComponent<ClosedDoor>();
            var animatorController = doorObject.GetComponent<DoorAnimatorController>();
            var audioController = doorObject.GetComponent<DoorAudioController>();
            var animator = door.GetComponent<Animator>();

            animatorController.Construct(animator);
            audioController.Construct(_staticData);

            WindowID popupWindow;
            ItemType keyType;

            (keyType, popupWindow) = typeId switch
            {
                UsableObjectTypeId.ClosedDoorOne => (ItemType.KeyOne, WindowID.DoorOneWindow),
                UsableObjectTypeId.ClosedDoorTwo => (ItemType.KeyTwo, WindowID.DoorTwoWindow),
                UsableObjectTypeId.ClosedDoorThree => (ItemType.KeyThree, WindowID.DoorThreeWindow),
                UsableObjectTypeId.ClosedDoorFour => (ItemType.KeyFour, WindowID.DoorFourWindow),
                UsableObjectTypeId.ClosedDoorFive => (ItemType.KeyFive, WindowID.DoorFiveWindow),
                UsableObjectTypeId.ClosedDoorSix => (ItemType.KeySix, WindowID.DoorSixWindow),
                _ => throw new ArgumentOutOfRangeException(nameof(typeId), typeId, null)
            };

            door.Construct(_inputService, _windowService, _inventoryHandler, animatorController, audioController,
                keyType, popupWindow);
        }
    }
}