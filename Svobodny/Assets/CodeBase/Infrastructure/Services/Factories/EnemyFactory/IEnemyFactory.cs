using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factories.EnemyFactory
{
    public interface IEnemyFactory : IService  
    {
        GameObject CreateEnemy(MonsterTypeID typeId, Vector3 position, Quaternion rotation);
    }
}
