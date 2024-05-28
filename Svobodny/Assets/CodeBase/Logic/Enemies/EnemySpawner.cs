using System.Collections.Generic;
using CodeBase.Data.StaticData.Monster;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using UnityEngine;

namespace CodeBase.Logic.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private IEnemyFactory _enemyFactory;

        public MonsterTypeID TypeID;
        public List<Vector3> Waypoints;

        public void Construct(IEnemyFactory enemyFactory, MonsterTypeID typeID, List<Vector3> waypoints)
        {
            _enemyFactory = enemyFactory;
            TypeID = typeID;
            Waypoints = waypoints;
        }

        public GameObject Spawn()
        {
            return _enemyFactory.CreateEnemy(TypeID, transform.position, transform.rotation);
        }
    }
}