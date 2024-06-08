using CodeBase.Logic.UsableObjects.Essentials;
using UnityEngine;

namespace CodeBase.Data.StaticData.UsableObjects
{
    [System.Serializable]
    public class EssentialSpawnerData
    {
        public string Id;
        public EssentialType EssentialType;
        public Quaternion Rotation;
        public Vector3 Position;
        public int Amount;

        public EssentialSpawnerData(string id, EssentialType essentialType, Quaternion rotation, Vector3 position, int amount)
        {
            Id = id;
            EssentialType = essentialType;
            Rotation = rotation;
            Position = position;
            Amount = amount;
        }
    }
}