using CodeBase.Logic.UsableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.UsableObjectFactory
{
    public interface IUsableObjectFactory : IService
    {
        GameObject CreateUsableObject(UsableObjectTypeId typeId, Vector3 position, Quaternion rotation);
    }
}