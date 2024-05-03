using System.Collections.Generic;
using CodeBase.Data.StaticData.Character;
using CodeBase.Data.StaticData.Guns;
using CodeBase.Data.StaticData.Items;
using CodeBase.Data.StaticData.Level;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Data.StaticData.Npc;
using CodeBase.Modules.Inventory;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        MonsterStaticData ForMonster(MonsterTypeID typeID);
        NpcStaticData ForNpc(NpcTypeId typeId);
        LevelStaticData ForLevel(string scene);
        CharacterStaticData ForCharacter();
        ItemStaticData ForItem(ItemType itemType);
        GunStaticData ForGun(GunType gunType);
    }
}