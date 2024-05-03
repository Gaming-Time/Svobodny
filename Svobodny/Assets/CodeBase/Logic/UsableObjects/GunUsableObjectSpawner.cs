using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects
{
    public class GunUsableObjectSpawner : MonoBehaviour
    {
        private IUsableObjectFactory _factory;
        
        public GunType GunType { get; private set; }

        public void Construct(IUsableObjectFactory factory, GunType gunType)
        {
            _factory = factory;
            GunType = gunType;
        }

        public GameObject Spawn() => _factory.CreateGunUsableObject(GunType, transform.position, transform.rotation);
    }
}