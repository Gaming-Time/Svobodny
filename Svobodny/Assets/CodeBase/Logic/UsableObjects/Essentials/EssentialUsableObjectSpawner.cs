using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Essentials
{
    public class EssentialUsableObjectSpawner : MonoBehaviour
    {
        private IUsableObjectFactory _usableObjectFactory;

        public EssentialType EssentialType { get; private set; }
        public int Amount { get; private set; }

        public void Construct(IUsableObjectFactory usableObjectFactory, EssentialType essentialType, int amount)
        {
            _usableObjectFactory = usableObjectFactory;
            EssentialType = essentialType;
            Amount = amount;
        }

        public GameObject Spawn() =>
            _usableObjectFactory.CreateEssentialUsableObject(EssentialType, transform.position, transform.rotation);
    }
}   