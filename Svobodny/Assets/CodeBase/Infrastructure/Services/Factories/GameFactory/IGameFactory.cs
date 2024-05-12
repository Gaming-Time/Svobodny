using CodeBase.Data.StaticData.Character;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Logic.UsableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreateCharacter(Vector3 position, Quaternion rotation, CharacterStaticData staticData);
        void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID);
        void CreateNpcSpawner(Vector3 position, Quaternion rotation, string spawnerId, NpcTypeId typeID);
        void SpawnAllMonsters();
        void SpawnAllNpcs();
        void InitCamera(GameObject character);
        void CreateObjectSpawner(Vector3 spawnerPosition, Quaternion spawnerRotation, string spawnerId,
            UsableObjectTypeId spawnerTypeId);
        void SpawnAllObjects();
        void Cleanup();
        void CreateInventoryHandler();
        void CreateUIHandler();
    }
}
