using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.StaticData.Character;
using CodeBase.Data.StaticData.Guns;
using CodeBase.Data.StaticData.Items;
using CodeBase.Data.StaticData.Level;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<NpcTypeId, NpcStaticData> _npcs;
        private Dictionary<ItemType, ItemStaticData> _items;
        private Dictionary<GunType, GunStaticData> _guns;
        private CharacterStaticData _character;


        public void LoadStaticData()
        {
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.StaticDataPath.Level)
                .ToDictionary(name => name.LevelKey, data => data);
            _monsters = Resources.LoadAll<MonsterStaticData>(AssetPath.StaticDataPath.Monster)
                .ToDictionary(monsterData => monsterData.TypeID, monsterData => monsterData);
            _npcs = Resources.LoadAll<NpcStaticData>(AssetPath.StaticDataPath.Npc)
                .ToDictionary(npcData => npcData.TypeId, npcData => npcData);
            _items = Resources.LoadAll<ItemStaticData>(AssetPath.StaticDataPath.Item)
                .ToDictionary(itemData => itemData.ItemType, itemData => itemData);
            _guns = Resources.LoadAll<GunStaticData>(AssetPath.StaticDataPath.Gun)
                .ToDictionary(gunData => gunData.GunType, gunData => gunData);
            _character = Resources.Load<CharacterStaticData>(AssetPath.StaticDataPath.Character);
        }

        public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
            _monsters.GetValueOrDefault(typeID);

        public NpcStaticData ForNpc(NpcTypeId typeId) =>
            _npcs.GetValueOrDefault(typeId);

        public LevelStaticData ForLevel(string scene) =>
            _levels.GetValueOrDefault(scene);

        public ItemStaticData ForItem(ItemType itemType) =>
            _items.GetValueOrDefault(itemType);

        public GunStaticData ForGun(GunType gunType) => _guns.GetValueOrDefault(gunType);

        public CharacterStaticData ForCharacter() => _character;
    }
}