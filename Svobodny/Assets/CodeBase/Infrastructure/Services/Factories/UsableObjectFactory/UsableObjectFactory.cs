using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Logic.UsableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UsableObjectFactory
{
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

            return _assetProvider.Instantiate(objectPath, position);
        }

        private static string GetObjectPath(UsableObjectTypeId typeId)
        {
            return typeId switch
            {
                UsableObjectTypeId.Wardrobe => AssetPath.ObjectsPath.WardrobePath,
                UsableObjectTypeId.Door => AssetPath.ObjectsPath.DoorPath,
                UsableObjectTypeId.RedKey => AssetPath.ObjectsPath.RedKeyPath,
                UsableObjectTypeId.ClosedDoorOne => AssetPath.ObjectsPath.DoorOnePath,
                UsableObjectTypeId.CloseDoorTwo => AssetPath.ObjectsPath.DoorTwoPath,
                UsableObjectTypeId.CloseDoorThree => AssetPath.ObjectsPath.DoorThreePath,
                _ => throw new ArgumentException(typeId + " не реализован в фабрике"),
            };
        }
    }
}