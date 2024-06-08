using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory.Slots;
using CodeBase.Modules.UI;
using UnityEngine;

namespace CodeBase.Modules.Inventory.Guns
{
    public class GunsUIHandler : MonoBehaviour
    {
        private InventoryHandler _inventoryHandler;
        private Hud _hud;
        private IAssets _assetProvider;
        private IStaticDataService _staticData;

        private List<GunSlot> _gunSlots = new();
        private GunSlot _selectedSlot;
        private Transform _knifeSlotContainer;
        private Transform _pistolSlotContainer;

        public void Construct(InventoryHandler inventoryHandler, Hud hud, IAssets assetProvider,
            IStaticDataService staticDataService)
        {
            _inventoryHandler = inventoryHandler;
            _hud = hud;
            _assetProvider = assetProvider;
            _staticData = staticDataService;

            Subscribe();
            Initialize();
        }

        private void OnDestroy()
        {
            if(!_inventoryHandler)
                return;
            
            CleanUp();
        }

        private void Initialize()
        {
            _knifeSlotContainer = _hud.KnifeSlotContainer;
            _pistolSlotContainer = _hud.PistolSlotContainer;
        }

        public void CleanUp() => UnSubscribe();

        private void UnSubscribe()
        {
            _inventoryHandler.GunAdded -= OnGunAdded;
            _inventoryHandler.GunRemoved -= OnGunRemoved;
            _inventoryHandler.GunSelected -= OnGunSelected;
        }

        private void Subscribe()
        {
            _inventoryHandler.GunAdded += OnGunAdded;
            _inventoryHandler.GunRemoved += OnGunRemoved;
            _inventoryHandler.GunSelected += OnGunSelected;
        }

        private void OnGunAdded(GunType gunType)
        {
            var slot = _assetProvider.Instantiate<GunSlot>(AssetPath.UIPath.GunSlot);
            var gunData = _staticData.ForGun(gunType);
            slot.Construct(gunType, gunData.Sprite);
            _gunSlots.Add(slot);

            switch (gunType)
            {
                case GunType.Knife:
                    slot.transform.SetParent(_knifeSlotContainer, false);
                    break;
                case GunType.Pistol:
                    slot.transform.SetParent(_pistolSlotContainer, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gunType), gunType, null);
            }
        }

        private void OnGunRemoved(GunType gunType)
        {
            var slot = _gunSlots.First(slot => slot.GunType == gunType);
            _gunSlots.Remove(slot);
            Destroy(slot.gameObject);
        }

        private void OnGunSelected(GunType? gunType)
        {
            _selectedSlot?.UnSelect();
            var slot = _gunSlots.FirstOrDefault(gun => gun.GunType == gunType);

            slot?.Select();
            _selectedSlot = slot;
        }
    }
}