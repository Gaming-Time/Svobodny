using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class ClosedDoorOne : UsableObject
    {
        protected override IInputService InputService { get; set; }
        private InventoryHandler _inventoryHandler;
        private DoorAnimatorController _animatorController;
        private IWindowService _windowService;

        private bool _isOpen;

        public void Construct(IInputService inputService, IWindowService windowService,
            InventoryHandler inventoryHandler,
            DoorAnimatorController animatorController)
        {
            InputService = inputService;
            _windowService = windowService;
            _inventoryHandler = inventoryHandler;
            _animatorController = animatorController;
        }

        public override void Use()
        {
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
                _isOpen = false;

                return;
            }

            if (!_inventoryHandler.HasItem(ItemType.KeyOne))
            {
                _windowService.OpenOrCreateWindow(WindowID.DoorOneWindow);

                return;
            }

            _animatorController.PlayOpenAnimation();
            _isOpen = true;
        }
    }
}