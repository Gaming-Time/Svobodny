using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic.UsableObjects.Essentials;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace CodeBase.Modules.Inventory.Essentials
{
    public class EssentialsUIHandler : MonoBehaviour
    {
        private InventoryHandler _inventoryHandler;
        private Hud _hud;
        private IAssets _assetProvider;
        private IStaticDataService _staticData;

        private Dictionary<EssentialType, EssentialSlot> _essentialSlots = new();
        private Transform _bulletSlotContainer;
        private Transform _medicineSlotContainer;

        public void Construct(InventoryHandler inventoryHandler, Hud hud, IAssets assetProvider,
            IStaticDataService staticDataService)
        {
            _inventoryHandler = inventoryHandler;
            _hud = hud;
            _assetProvider = assetProvider;
            _staticData = staticDataService;
            
            Initialize();
            Subscribe();
        }

        private void OnDestroy()
        {
            if(!_inventoryHandler)
                return;
            
            Cleanup();
        }

        private void Initialize()
        {
            _bulletSlotContainer = _hud.BulletSlotContainer;
            _medicineSlotContainer = _hud.MedicineSlotContainer;
        }

        private void Subscribe()
        {
            _inventoryHandler.EssentialAdded += OnEssentialAdded;
            _inventoryHandler.EssentialRemoved += OnEssentialRemoved;
        }

        private void Cleanup()
        {
            _inventoryHandler.EssentialAdded -= OnEssentialAdded;
            _inventoryHandler.EssentialRemoved -= OnEssentialRemoved;
        }

        private void OnEssentialAdded(EssentialType essentialType, int amount)
        {
            if (_essentialSlots.TryGetValue(essentialType, out var slot))
            {
                slot.AddAmount(amount);
                return;
            }

            slot = _assetProvider.Instantiate<EssentialSlot>(AssetPath.UIPath.EssentialSlot);
            var data = _staticData.ForEssential(essentialType);
            slot.Construct(data.Sprite, amount);
            _essentialSlots.Add(essentialType, slot);

            var parent = essentialType switch
            {
                EssentialType.Bullet => _bulletSlotContainer,
                EssentialType.Medicine => _medicineSlotContainer,
                _ => throw new ArgumentOutOfRangeException(nameof(essentialType), essentialType, null)
            };
            
            slot.transform.SetParent(parent, false);
        }

        private void OnEssentialRemoved(EssentialType essentialType, int amount)
        {
            Assert.IsTrue(_essentialSlots.TryGetValue(essentialType, out var slot));

            if (slot.Amount - amount <= 0)
            {
                _essentialSlots.Remove(essentialType);
                Destroy(slot.gameObject);
                
                return;
            }
            
            slot.ReduceAmount(amount);
        }
    }
}