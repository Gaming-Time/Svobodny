using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Monster
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeID TypeId;
        public Quaternion Rotation;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterTypeID typeId, Quaternion rotation, Vector3 position)
        {
            Id = id;
            TypeId = typeId;
            Rotation = rotation;
            Position = position;
        }
    }
}