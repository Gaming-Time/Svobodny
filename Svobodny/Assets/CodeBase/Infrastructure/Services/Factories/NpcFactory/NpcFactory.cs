using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.NpcFactory
{
    public class NpcFactory : INpcFactory
    {
        private IAssets _assetProvider;

        public NpcFactory(IAssets assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateNpc(NpcTypeId typeId, Vector3 position, Quaternion rotation)
        {
            string path = GetNpcPath(typeId);

            return _assetProvider.Instantiate(path, position, rotation);
        }

        private string GetNpcPath(NpcTypeId typeId)
        {
            return typeId switch
            {
                NpcTypeId.Test => AssetPath.TestNpcPath,
                _ => throw new ArgumentException($"{typeId} не определён в фабрике")
            };
        }
    }
}
