using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic.UsableObjects;
using CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UsableObjectFactory
{
    public interface IUsableObjectFactory : IService
    {
        GameObject CreateUsableObject(UsableObjectTypeId typeId, Vector3 position, Quaternion rotation);
    }

    public class UsableObjectFactory : IUsableObjectFactory
    {
        private readonly IAssets _assetProvider;

        public UsableObjectFactory(IAssets assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateUsableObject(UsableObjectTypeId typeId, Vector3 position, Quaternion rotation)
        {
            string objectPath = GetObjectPath(typeId);

            return _assetProvider.Instantiate(objectPath, position, rotation);
        }

        private static string GetObjectPath(UsableObjectTypeId typeId)
        {
            return typeId switch
            {
                UsableObjectTypeId.Wardrobe => AssetPath.ObjectsPath.WardrobePath,
                _ => throw new ArgumentException(typeId + " не реализован в фабрике"),
            };
        }
    }
}