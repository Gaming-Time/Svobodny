using System.Collections.Generic;
using CodeBase.Data.StaticData.Character;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Logic;
using CodeBase.Logic.UsableObjects;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreateCharacter(Vector3 position, Quaternion rotation, CharacterStaticData staticData);

        void CreateEnemySpawner(Vector3 position, Quaternion rotation, string spawnerID, MonsterTypeID typeID,
            List<Vector3> waypoints);

        void CreateNpcSpawner(Vector3 position, Quaternion rotation, string spawnerId, NpcTypeId typeID);
        void SpawnAllMonsters();
        void SpawnAllNpcs();
        void InitCamera(GameObject character);

        void CreateObjectSpawner(Vector3 spawnerPosition, Quaternion spawnerRotation, string spawnerId,
            UsableObjectTypeId spawnerTypeId);

        void SpawnAllObjects();
        void Cleanup();
        void CreateItemsUIHandler();
        void CreateHud();
        void CreateGunsUiHandler();

        void CreateGunObjectSpawner(Vector3 spawnerPosition, Quaternion spawnerRotation, string spawnerId,
            GunType gunType);

        void SpawnGuns();
        void CreateHealthUIHandler();
        void InitTriggers();
    }
}