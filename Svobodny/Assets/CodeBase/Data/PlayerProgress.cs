using System;
using System.Collections.Generic;
using CodeBase.Modules.Character.UI;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public PositionOnLevel PositionOnLevel;
        public InventoryData InventoryData;

        public PlayerProgress(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            InventoryData = new InventoryData();
        }
    }

    [Serializable]
    public class InventoryData
    {
        public List<Gun> Guns = new();
    }

    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data Position;
        public Vector3Data Rotation;

        public PositionOnLevel(string initialLevel)
        {
            Level = initialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position, Vector3Data rotation)
        {
            Level = level;
            Position = position;
            Rotation = rotation;
        }
    }

    [Serializable]
    public class Vector3Data
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}