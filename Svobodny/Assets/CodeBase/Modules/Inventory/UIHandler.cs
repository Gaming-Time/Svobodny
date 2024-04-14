using System.Collections.Generic;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Modules.Inventory.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Modules.Inventory
{
    public class UIHandler : MonoBehaviour
    {
        private InventoryHandler _inventoryHandler;
        private IStaticDataService _staticData;
        private IAssets _assetProvider;
        private IUIFactory _uiFactory;

        private Dictionary<ItemType, Slot> _slots = new();
        private Transform _scrollRectangleTransform;

        public void Construct(InventoryHandler inventoryHandler, IStaticDataService staticData, IAssets assetProvider,
            IUIFactory uiFactory)
        {
            _inventoryHandler = inventoryHandler;
            _staticData = staticData;
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;

            Subscribe();
            Initialize();
        }

        private void Initialize()
        {
            var inventoryRoot = _uiFactory.CreateItemsInventory();
            _scrollRectangleTransform = inventoryRoot.GetComponentInChildren<VerticalLayoutGroup>().transform;
        }

        public void Cleanup()
        {
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            _inventoryHandler.ItemAdded -= OnItemAdded;
            _inventoryHandler.ItemRemoved -= OnItemRemoved;
        }

        private void Subscribe()
        {
            _inventoryHandler.ItemAdded += OnItemAdded;
            _inventoryHandler.ItemRemoved += OnItemRemoved;
        }

        private void OnItemRemoved(ItemType itemType)
        {
            
        }

        private void OnItemAdded(ItemType itemType)
        {
            if (!_slots.TryGetValue(itemType, out var slot))
            {
                slot = _assetProvider.Instantiate<Slot>(AssetPath.UIPath.Slot, _scrollRectangleTransform);
                var sprite = _staticData.ForItem(itemType).Sprite;
                slot.Construct(sprite, 1);
                _slots.Add(itemType, slot);

                return;
            }

            slot.AddItem(1);
        }

        public void AddItemView()
        {
        }

        public void RemoveItemView()
        {
        }
    }
}