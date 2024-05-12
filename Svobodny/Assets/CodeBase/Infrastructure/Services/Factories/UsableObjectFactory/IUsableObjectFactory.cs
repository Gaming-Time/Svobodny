using CodeBase.Logic.UsableObjects;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UsableObjectFactory
{
    public interface IUsableObjectFactory : IService
    {
        GameObject CreateUsableObject(UsableObjectTypeId typeId, Vector3 position, Quaternion rotation);
        GameObject CreateGunUsableObject(GunType gunType, Vector3 position, Quaternion rotation);
    }
}