using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects
{
    public class UsableObjectSpawner : MonoBehaviour
    {
        private IUsableObjectFactory _factory;
        private UsableObjectTypeId _typeId;

        public void Construct(IUsableObjectFactory factory, UsableObjectTypeId typeId)
        {
            _factory = factory;
            _typeId = typeId;
        }

        public GameObject Spawn() => _factory.CreateUsableObject(_typeId, transform.position, transform.rotation);
    }
}