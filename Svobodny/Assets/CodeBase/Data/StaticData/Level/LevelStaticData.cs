using System.Collections.Generic;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Data.StaticData.UsableObjects;
using UnityEngine;

namespace CodeBase.Data.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "LevelData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public List<NpcSpawnerData> NpcSpawners;
        public List<ObjectSpawnerData> ObjectsSpawners;

        public Vector3 DayPlayerPosition;
        public Quaternion DayPlayerRotation;
        public Vector3 NightPlayerPosition;
        public Quaternion NightPlayerRotation;

        public LevelPhase initialPhase;
    }
}