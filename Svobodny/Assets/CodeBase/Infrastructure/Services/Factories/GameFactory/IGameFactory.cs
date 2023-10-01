using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateCharacter(Vector3 position, Quaternion rotation);
    void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID);
    void CreateNpcSpawner(Vector3 position, Quaternion rotation, string spawnerId, NpcTypeId typeID);
    void SpawnAllMonsters();
    void SpawnAllNpcs();

}
