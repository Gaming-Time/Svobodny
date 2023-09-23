using System.Collections.Generic;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "LevelData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public List<NpcSpawnerData> NpcSpawners;
        public Transform DayPlayerTransform;
        public Transform NightPlayerTrabsform;
    }
}