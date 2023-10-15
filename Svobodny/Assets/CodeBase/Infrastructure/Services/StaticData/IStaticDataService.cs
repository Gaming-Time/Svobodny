using CodeBase.Infrastructure.Services.StaticData.Character;
using CodeBase.Infrastructure.Services.StaticData.Level;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        MonsterStaticData ForMonster(MonsterTypeID typeID);
        NpcStaticData ForNpc(NpcTypeId typeId);
        LevelStaticData ForLevel(string scene);
        CharacterStaticData ForCharacter();
    }
}