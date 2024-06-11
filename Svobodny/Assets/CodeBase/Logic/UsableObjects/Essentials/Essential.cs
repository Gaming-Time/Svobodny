using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.UI;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Essentials
{
    public class Essential : UsableObject
    {
        protected override IInputService InputService { get; set; }

        private InventoryHandler _inventoryHandler;
        private int _amount;
        private EssentialType _essentialType;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler, EssentialType essentialType, int amount)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
            _essentialType = essentialType;
            _amount = amount;
        }
        public override void Use()
        {
            _inventoryHandler.AddEssential(_essentialType, _amount);
            Destroy(gameObject);
        }
    }
}