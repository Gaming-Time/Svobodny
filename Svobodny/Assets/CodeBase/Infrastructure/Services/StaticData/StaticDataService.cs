using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.StaticData.Level;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<NpcTypeId, NpcStaticData> _npcs;

        public void LoadStaticData()
        {
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.StaticDataPath.Level)
                .ToDictionary(name => name.LevelKey, data => data);
            _monsters = Resources.LoadAll<MonsterStaticData>(AssetPath.StaticDataPath.Monster)
                .ToDictionary(monsterData => monsterData.TypeID, monsterData => monsterData);
            _npcs = Resources.LoadAll<NpcStaticData>(AssetPath.StaticDataPath.Npc)
                .ToDictionary(npcData => npcData.TypeId, npcData => npcData);
        }

        public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
            _monsters.TryGetValue(typeID, out var staticData) ? staticData : null;

        public NpcStaticData ForNpc(NpcTypeId typeId) =>
            _npcs.TryGetValue(typeId, out var staticData) ? staticData : null;

        public LevelStaticData ForLevel(string scene) =>
            _levels.TryGetValue(scene, out var staticData) ? staticData : null;
    }
}