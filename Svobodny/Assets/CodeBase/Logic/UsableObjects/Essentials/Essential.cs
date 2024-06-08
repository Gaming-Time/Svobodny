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

        public void Construct(IInputService inputService, EssentialType essentialType, int amount)
        {
            InputService = inputService;
            _essentialType = essentialType;
            _amount = amount;
        }
        public override void Use()
        {
            Debug.LogWarning($"{_amount} of {_essentialType} used");
            Destroy(gameObject);
        }
    }
}