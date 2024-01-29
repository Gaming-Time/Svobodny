using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects
{
    public class UsableObjectSpawner : MonoBehaviour
    {
        private IUsableObjectFactory _factory;

        public UsableObjectTypeId TypeId { get; private set; }

        public void Construct(IUsableObjectFactory factory, UsableObjectTypeId typeId)
        {
            _factory = factory;
            TypeId = typeId;
        }

        public GameObject Spawn() => _factory.CreateUsableObject(TypeId, transform.position, transform.rotation);
    }
}