using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Npc
{
    [Serializable]
    public class NpcSpawnerData
    {
        public string Id;
        public NpcTypeId TypeId;
        public Quaternion Rotation;
        public Vector3 Position;

        public NpcSpawnerData(string id, NpcTypeId typeId, Quaternion rotation, Vector3 position)
        {
            Id = id;
            TypeId = typeId;
            Rotation = rotation;
            Position = position;
        }
    }
}