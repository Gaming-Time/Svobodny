using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Data.StaticData.Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Static Data/Items", order = 0)]
    public class ItemStaticData : ScriptableObject
    {
        public ItemType ItemType;
        public Sprite Sprite;
    }
}