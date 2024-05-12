using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Data.StaticData.UsableObjects
{
    [System.Serializable]
    public class GunSpawnerData
    {
        public string Id;
        public GunType GunType;
        public Quaternion Rotation;
        public Vector3 Position;

        public GunSpawnerData(string id, GunType gunType, Quaternion rotation, Vector3 position)
        {
            Id = id;
            GunType = gunType;
            Rotation = rotation;
            Position = position;
        }
    }
}