using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Monster
{
    [CreateAssetMenu(menuName = "Static Data/Monster", fileName = "MonsterData", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeID TypeID;

        [Range(1, 100)] public int Health;

        public GameObject Prefab;
    }
}