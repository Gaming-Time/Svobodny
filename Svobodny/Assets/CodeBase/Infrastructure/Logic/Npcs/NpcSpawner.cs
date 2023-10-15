using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.Npcs
{
    public class NpcSpawner : MonoBehaviour
    {

        private INpcFactory _npcFactory;

        public NpcTypeId TypeID;

        public void Construct(INpcFactory npcFactory)
        {
            _npcFactory = npcFactory;
        }

        public GameObject Spawn()
        {
            return _npcFactory.CreateNpc(TypeID, transform.position, transform.rotation);
        }
    }
}