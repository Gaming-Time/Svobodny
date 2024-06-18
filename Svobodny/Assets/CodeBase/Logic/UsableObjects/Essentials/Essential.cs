using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.UI;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Essentials
{
    public class Essential : UsableObject
    {
        protected override IInputService InputService { get; set; }
        protected override IWindowService WindowService { get; set; }

        private InventoryHandler _inventoryHandler;
        private int _amount;
        private EssentialType _essentialType;

        public void Construct(IInputService inputService, IWindowService windowService, InventoryHandler inventoryHandler, EssentialType essentialType, int amount)
        {
            InputService = inputService;
            WindowService = windowService;
            _inventoryHandler = inventoryHandler;
            _essentialType = essentialType;
            _amount = amount;
        }

        protected override void Use()
        {
            base.Use();
            _inventoryHandler.AddEssential(_essentialType, _amount);
            Destroy(gameObject);
        }
    }
}