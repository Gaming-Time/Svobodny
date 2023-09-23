using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Npc
{
    [CreateAssetMenu(menuName = "Static Data/NPC", fileName = "NpcStaticData", order = 0)]
    public class NpcStaticData : ScriptableObject
    {
        public NpcTypeId TypeId;
        public GameObject Prefab;
    }
}