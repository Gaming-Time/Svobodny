using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class Door : UsableObject
    {
        private DoorAnimatorController _animatorController;

        private bool _isOpen;

        protected override IInputService InputService { get; set; }
        private InventoryHandler _inventoryHandler;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler,
            DoorAnimatorController animatorController)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
            _animatorController = animatorController;
        }

        public override void Use()
        {
            
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
            }
            else
            {
                _animatorController.PlayOpenAnimation();
            }

            _isOpen = !_isOpen;
        }
    }
}