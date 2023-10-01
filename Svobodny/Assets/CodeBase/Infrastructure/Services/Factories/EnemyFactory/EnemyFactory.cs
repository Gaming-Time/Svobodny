﻿using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using System;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factories.EnemyFactory
{
    public class EnemyFactory : IEnemyFactory
    {
        private IAssets _assetProvider;

        public EnemyFactory(IAssets assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateEnemy(MonsterTypeID typeId, Vector3 position, Quaternion rotation)
        {
            string enemyPath = GetEnemyPath(typeId);

            return _assetProvider.Instantiate(enemyPath, position, rotation);
        }

        private string GetEnemyPath(MonsterTypeID typeId)
        {
            return typeId switch
            {
                MonsterTypeID.Test => AssetPath.TestEnemyPath,
                _ => throw new ArgumentException(typeId.ToString() + " не реализован в фабрике"),
            };
        }
    }
}
