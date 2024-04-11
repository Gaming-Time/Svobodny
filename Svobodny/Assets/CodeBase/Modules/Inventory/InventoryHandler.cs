using System.Collections.Generic;
using CodeBase.Infrastructure.Logic.UsableObjects.Key;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private List<Item> _items = new();

        public void AddKey(KeyType keyType)
        {
        }
        public void AddItem(Item item){}
        public void RemoveItem(Item item){}
    }

    public class UIHandler : MonoBehaviour
    {
        public void AddItemView(){}
        public void RemoveItemView(){}
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
        RedKey
    }
}