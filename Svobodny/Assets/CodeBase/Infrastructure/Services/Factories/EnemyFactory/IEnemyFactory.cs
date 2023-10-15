using CodeBase.Infrastructure.Services.StaticData.Monster;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.EnemyFactory
{
    public interface IEnemyFactory : IService  
    {
        GameObject CreateEnemy(MonsterTypeID typeId, Vector3 position, Quaternion rotation);
    }
}
