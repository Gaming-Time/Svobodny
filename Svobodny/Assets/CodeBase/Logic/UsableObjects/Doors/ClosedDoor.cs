using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class ClosedDoor : UsableObject
    {
        protected override IInputService InputService { get; set; }
        private InventoryHandler _inventoryHandler;
        private DoorAnimatorController _animatorController;
        private IWindowService _windowService;
        private ItemType _itemType;
        private WindowID _windowId;

        private bool _isOpen;

        public void Construct(IInputService inputService, IWindowService windowService,
            InventoryHandler inventoryHandler,
            DoorAnimatorController animatorController, ItemType itemType, WindowID windowID)
        {
            InputService = inputService;
            _windowService = windowService;
            _inventoryHandler = inventoryHandler;
            _animatorController = animatorController;
            _itemType = itemType;
            _windowId = windowID;
        }

        public override void Use()
        {
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
                _isOpen = false;

                return;
            }

            if (!_inventoryHandler.HasItem(_itemType))
            {
                _windowService.OpenOrCreateWindow(_windowId);

                return;
            }

            _animatorController.PlayOpenAnimation();
            _isOpen = true;
        }
    }
}