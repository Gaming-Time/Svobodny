using System.Collections.Generic;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using CodeBase.Infrastructure.Services.StaticData.UsableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "LevelData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public List<NpcSpawnerData> NpcSpawners;
        public List<ObjectStaticData> ObjectsSpawners;

        public Vector3 DayPlayerPosition;
        public Quaternion DayPlayerRotation;
        public Vector3 NightPlayerPosition;
        public Quaternion NightPlayerRotation;

        public LevelPhase initialPhase;
    }
}