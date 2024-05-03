using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private IInputService _inputService;
        
        private List<Item> _items = new();
        private List<Gun> _guns = new();

        private GunType? _selectedGun;

        public event Action<ItemType> ItemAdded;
        public event Action<ItemType> ItemRemoved;

        public event Action<GunType> GunAdded;
        public event Action<GunType> GunRemoved;
        public event Action<GunType?> GunSelected;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;

            Initialize();
        }

        private void Initialize()
        {
            SelectGun(null);
        }

        public void AddItem(ItemType itemType)
        {
            _items.Add(new Item(itemType));
            ItemAdded?.Invoke(itemType);
        }

        public void RemoveItem(ItemType itemType)
        {
            var item = _items.FirstOrDefault(item => item.ItemType == itemType);
            if (item == null)
                return;

            _items.Remove(item);
            ItemRemoved?.Invoke(itemType);
        }

        public void AddGun(GunType gunType)
        {
            if(_guns.Exists(gun => gun.GunType == gunType))
                return;
            
            _guns.Add(new Gun(gunType));
            GunAdded?.Invoke(gunType);

            if (_selectedGun == null)
                SelectGun(gunType);
        }

        public void RemoveGun(GunType gunType)
        {
            var gun = _guns.FirstOrDefault(gun => gun.GunType == gunType);
            if(gun == null)
                return;

            _guns.Remove(gun);
            GunRemoved?.Invoke(gunType);
            
            if(_selectedGun == gunType)
                SelectGun(null);
        }
        
        public bool HasItem(ItemType itemType) => _items.Exists(item => item.ItemType == itemType);
        
        private void SelectGun(GunType? gunType)
        {
            _selectedGun = gunType;
            GunSelected?.Invoke(gunType);
        }
    }

    [Serializable]
    public class Item
    {
        public ItemType ItemType;

        public Item(ItemType itemType)
        {
            ItemType = itemType;
        }
    }

    [Serializable]
    public class Gun
    {
        public GunType GunType;

        public Gun(GunType gunType)
        {
            GunType = gunType;
        }
    }

    public enum GunType
    {
        Knife,
        Pistol
    }

    public enum ItemType
    {
        None,
        KeyOne,
        KeyTwo,
        KeyThree,
        KeyFour,
        KeyFive,
        KeySix,
    }
}