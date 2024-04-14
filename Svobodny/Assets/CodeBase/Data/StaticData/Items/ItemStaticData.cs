using System.Collections.Generic;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Data.StaticData.Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "StaticData/Items", order = 0)]
    public class ItemStaticData : ScriptableObject
    {
        public ItemType ItemType;
        public Sprite Sprite;
    }

   
}