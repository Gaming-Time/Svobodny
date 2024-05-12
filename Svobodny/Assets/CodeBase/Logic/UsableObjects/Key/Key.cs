using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects.Key
{
    public class Key : UsableObject
    {
        protected override IInputService InputService { get; set; }

        private InventoryHandler _inventoryHandler;
        private ItemType _itemType;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler, ItemType itemType)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
            _itemType = itemType;
        }
        public override void Use()
        {
            _inventoryHandler.AddItem(_itemType);
            Destroy(gameObject);
        }
    }
}