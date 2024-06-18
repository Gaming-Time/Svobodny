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
        protected override IWindowService WindowService { get; set; }

        private InventoryHandler _inventoryHandler;
        private DoorAnimatorController _animatorController;
        private DoorAudioController _audioController;
        private ItemType _itemType;
        private WindowID _windowId;

        private bool _isOpen;

        public void Construct(IInputService inputService, IWindowService windowService,
            InventoryHandler inventoryHandler,
            DoorAnimatorController animatorController, DoorAudioController audioController, ItemType itemType, WindowID windowID)
        {
            InputService = inputService;
            WindowService = windowService;
            _inventoryHandler = inventoryHandler;
            _animatorController = animatorController;
            _audioController = audioController;
            _itemType = itemType;
            _windowId = windowID;
        }

        protected override void Use()
        {
            base.Use();
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
                _audioController.PlayCloseSound();
                _isOpen = false;

                return;
            }

            if (!_inventoryHandler.HasItem(_itemType))
            {
                WindowService.OpenOrCreateWindow(_windowId);

                return;
            }

            _animatorController.PlayOpenAnimation();
            _audioController.PlayOpenSound();
            
            _isOpen = true;
        }

        protected override void ShowInteractionButton()
        {
        }
    }
}