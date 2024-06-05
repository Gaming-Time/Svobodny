using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.UI;

namespace CodeBase.Logic.UsableObjects
{
    public class Gun : UsableObject
    {
        protected override IInputService InputService { get; set; }

        private InventoryHandler _inventoryHandler;
        private IWindowService _windowService;
        private GunType _gunType;

        public void Construct(IInputService inputService, IWindowService windowService,
            InventoryHandler inventoryHandler, GunType gunType)
        {
            InputService = inputService;
            _windowService = windowService;
            _inventoryHandler = inventoryHandler;
            _gunType = gunType;
        }

        public override void Use()
        {
            if (_gunType == GunType.Knife)
                _windowService.OpenOrCreateWindow(WindowID.KnifeDialog);

            _inventoryHandler.AddGun(_gunType);
            Destroy(gameObject);
        }
    }
}