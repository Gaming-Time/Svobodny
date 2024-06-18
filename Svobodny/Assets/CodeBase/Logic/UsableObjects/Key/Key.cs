using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects.Key
{
    public class Key : UsableObject
    {
        protected override IInputService InputService { get; set; }
        protected override IWindowService WindowService { get; set; }

        private InventoryHandler _inventoryHandler;
        private ItemType _itemType;

        public void Construct(IInputService inputService, IWindowService windowService, InventoryHandler inventoryHandler, ItemType itemType)
        {
            InputService = inputService;
            WindowService = windowService;
            _inventoryHandler = inventoryHandler;
            _itemType = itemType;
        }

        protected override void Use()
        {
            base.Use();
            _inventoryHandler.AddItem(_itemType);
            Destroy(gameObject);
        }
    }
}