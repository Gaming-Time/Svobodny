using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.UI;

namespace CodeBase.Logic.UsableObjects
{
    public class Gun : UsableObject
    {
        protected override IInputService InputService { get; set; }
        protected override IWindowService WindowService { get; set; }

        private InventoryHandler _inventoryHandler;
        private GunType _gunType;

        public void Construct(IInputService inputService, IWindowService windowService,
            InventoryHandler inventoryHandler, GunType gunType)
        {
            InputService = inputService;
            WindowService = windowService;
            _inventoryHandler = inventoryHandler;
            _gunType = gunType;
        }

        protected override void Use()
        {
            base.Use();
            if (_gunType == GunType.Knife)
                WindowService.OpenOrCreateWindow(WindowID.KnifeDialog);

            _inventoryHandler.AddGun(_gunType);
            Destroy(gameObject);
        }
    }
}