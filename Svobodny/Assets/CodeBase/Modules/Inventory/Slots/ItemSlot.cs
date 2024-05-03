using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace CodeBase.Modules.Inventory.Slots
{
    public class ItemSlot : MonoBehaviour
    {
        public ItemType ItemType;
        public Image ItemSprite;
        public TextMeshProUGUI ItemCount;

        public void Construct(ItemType itemType, Sprite sprite, int count)
        {
            ItemSprite.sprite = sprite;
            ItemCount.text = count.ToString();
            ItemType = itemType;
        }

        public void AddItem(int count)
        {
            var currentCount = int.Parse(ItemCount.text);
            ItemCount.text = (currentCount + count).ToString();
        }
    }
}