using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Data.StaticData.Guns
{
    [CreateAssetMenu(fileName = "Gun Data", menuName = "Static Data/Guns", order = 0)]
    public class GunStaticData : ScriptableObject
    {
        public GunType GunType;
        public Sprite Sprite;
    }
}