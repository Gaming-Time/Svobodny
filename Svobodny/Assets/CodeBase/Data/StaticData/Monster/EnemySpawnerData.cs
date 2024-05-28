using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.StaticData.Monster
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeID TypeId;
        public Quaternion Rotation;
        public Vector3 Position;
        public List<Vector3> Waypoints;

        public EnemySpawnerData(string id, MonsterTypeID typeId, Quaternion rotation, Vector3 position, List<Vector3> waypoints)
        {
            Id = id;
            TypeId = typeId;
            Rotation = rotation;
            Position = position;
            Waypoints = waypoints;
        }
    }
}