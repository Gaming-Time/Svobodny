using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private IEnemyFactory _enemyFactory;

        public MonsterTypeID TypeID;

        public void Construct(IEnemyFactory enemyFactory, MonsterTypeID typeID)
        {
            _enemyFactory = enemyFactory;
            TypeID = typeID;
        }

        public GameObject Spawn()
        {
            return _enemyFactory.CreateEnemy(TypeID, transform.position, transform.rotation);
        }
    }
}