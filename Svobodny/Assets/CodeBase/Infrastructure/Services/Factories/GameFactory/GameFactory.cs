using Assets.CodeBase.Infrastructure.Logic.Enemies;
using Assets.CodeBase.Infrastructure.Logic.Npcs;
using Assets.CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using Assets.CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private IAssets _assetProvider;
        private IEnemyFactory _enemyFactory;
        private INpcFactory _npcFactory;

        private Dictionary<string, EnemySpawner> _enemySpawners = new();
        private Dictionary<string, NpcSpawner> _npcSpawners = new();

        public GameFactory(IAssets assetProvider, IEnemyFactory enemyFactory, INpcFactory npcFactory)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _npcFactory = npcFactory;
        }

        public GameObject CreateCharacter(Vector3 position, Quaternion rotation)
        {
            return _assetProvider.Instantiate(AssetPath.CharacterPath, position, rotation);
        }

        public void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID)
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawnerPath, position, rotation).GetComponent<EnemySpawner>();
            spawner.Construct(_enemyFactory);
            _enemySpawners.Add(spawnerID,spawner);
        }

        public void CreateNpcSpawner(Vector3 position, Quaternion rotation, string spawnerId, NpcTypeId typeID)
        {
            NpcSpawner spawner = _assetProvider.Instantiate(AssetPath.NpcSpawnerPath, position,rotation).GetComponent<NpcSpawner>();
            spawner.Construct(_npcFactory);
            _npcSpawners.Add(spawnerId,spawner);
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
            foreach(var spawner in _npcSpawners)
            {
                spawner.Value.Spawn();
            }
        }
    }
}
