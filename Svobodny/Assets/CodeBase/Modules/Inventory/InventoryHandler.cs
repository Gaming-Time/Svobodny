using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private List<Item> _items = new();

        public event Action<ItemType> ItemAdded;
        public event Action<ItemType> ItemRemoved;

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

        public bool HasItem(ItemType itemType) => _items.Exists(item => item.ItemType == itemType);
    }

    [System.Serializable]
    public class Item
    {
        public ItemType ItemType;

        public Item(ItemType itemType)
        {
            ItemType = itemType;
        }
    }

    public enum ItemType
    {
        None,
        KeyOne,
        KeyTwo,
        KeyThree
    }
}