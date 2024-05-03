using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects
{
    public class Gun : UsableObject
    {
        protected override IInputService InputService { get; set; }

        private InventoryHandler _inventoryHandler;
        private GunType _gunType;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler, GunType gunType)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
            _gunType = gunType;
        }
        
        public override void Use()
        {
            _inventoryHandler.AddGun(_gunType);
            Destroy(gameObject);
        }
    }
}