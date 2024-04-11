using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Inventory;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Key
{
    public abstract class KeyUsableObject : UsableObject
    {
        private InventoryHandler _inventoryHandler;
        protected override IInputService InputService { get; set; }

        protected abstract KeyType KeyType { get; }

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler)
        {
            InputService = inputService;
            _inventoryHandler = inventoryHandler;
        }

        public override void Use()
        {
            _inventoryHandler.AddKey(KeyType);
            Destroy(gameObject);
        }
    }

    public enum KeyType
    {
        Red,
    }

    public class RedKey : KeyUsableObject
    {
        protected override KeyType KeyType => KeyType.Red;
    }
}