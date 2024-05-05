using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Modules.Inventory.Slots
{
    public class GunSlot : MonoBehaviour
    {
        public GunType GunType;
        public Image Image;
        public Image Frame;

        public void Construct(GunType gunType, Sprite sprite)
        {
            GunType = gunType;
            Image.sprite = sprite;
        }

        public void Select()
        {
            Frame.enabled = true;
        }

        public void UnSelect()
        {
            Frame.enabled = false;
        }
    }
}