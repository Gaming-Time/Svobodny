using System;
using CodeBase.Infrastructure.Logic.UsableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.UsableObjects
{
    [Serializable]
    public class ObjectSpawnerData
    {
        public string Id;
        public UsableObjectTypeId TypeId;
        public Quaternion Rotation;
        public Vector3 Position;

        public ObjectSpawnerData(string id, UsableObjectTypeId typeId, Quaternion rotation, Vector3 position)
        {
            Id = id;
            TypeId = typeId;
            Rotation = rotation;
            Position = position;
        }
    }
}