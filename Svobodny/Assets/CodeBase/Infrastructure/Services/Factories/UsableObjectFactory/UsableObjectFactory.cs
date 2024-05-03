using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Logic.UsableObjects;
using CodeBase.Modules.Inventory;
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

        public GameObject CreateGunUsableObject(GunType gunType, Vector3 position, Quaternion rotation)
        {
            var objectPath = gunType switch
            {
                GunType.Knife => AssetPath.ObjectsPath.Guns.KnifePath,
                GunType.Pistol => AssetPath.ObjectsPath.Guns.PistolPath,
                _ => throw new ArgumentOutOfRangeException(nameof(gunType), gunType, null)
            };

            return _assetProvider.Instantiate(objectPath, position, rotation);
        }

        private static string GetObjectPath(UsableObjectTypeId typeId)
        {
            return typeId switch
            {
                UsableObjectTypeId.ClosedDoorOne or UsableObjectTypeId.ClosedDoorTwo
                    or UsableObjectTypeId.ClosedDoorThree or UsableObjectTypeId.ClosedDoorFour
                    or UsableObjectTypeId.ClosedDoorFive or UsableObjectTypeId.ClosedDoorSix
                    => AssetPath.ObjectsPath.ClosedDoorPath,
                UsableObjectTypeId.Wardrobe => AssetPath.ObjectsPath.WardrobePath,
                UsableObjectTypeId.Door => AssetPath.ObjectsPath.DoorPath,
                UsableObjectTypeId.KeyOne => AssetPath.ObjectsPath.KeyOnePath,
                UsableObjectTypeId.KeyTwo => AssetPath.ObjectsPath.KeyTwoPath,
                UsableObjectTypeId.KeyThree => AssetPath.ObjectsPath.KeyThreePath,
                UsableObjectTypeId.KeyFour => AssetPath.ObjectsPath.KeyFourPath,
                UsableObjectTypeId.KeyFive => AssetPath.ObjectsPath.KeyFivePath,
                UsableObjectTypeId.KeySix => AssetPath.ObjectsPath.KeySixPath,

                _ => throw new ArgumentException(typeId + " не реализован в фабрике"),
            };
        }
        
    }
}