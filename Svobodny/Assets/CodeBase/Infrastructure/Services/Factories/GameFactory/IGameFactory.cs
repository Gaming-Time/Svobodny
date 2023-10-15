using CodeBase.Infrastructure.Services.StaticData.Character;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
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
    }
}
