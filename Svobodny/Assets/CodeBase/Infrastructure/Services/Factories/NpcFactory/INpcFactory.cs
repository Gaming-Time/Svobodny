using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.NpcFactory
{
    public interface INpcFactory : IService
    {
        GameObject CreateNpc(NpcTypeId typeId, Vector3 position, Quaternion rotation);
    }
}
