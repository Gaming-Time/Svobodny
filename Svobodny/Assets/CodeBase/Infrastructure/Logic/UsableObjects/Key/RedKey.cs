using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Key
{
    public class RedKey : UsableObject
    {
        protected override IInputService InputService { get; set; }

        private InventoryHandler _inventoryHandler;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
        }
        public override void Use()
        {
            _inventoryHandler.AddKey(KeyType.Red);
            Destroy(gameObject);
        }
    }
}