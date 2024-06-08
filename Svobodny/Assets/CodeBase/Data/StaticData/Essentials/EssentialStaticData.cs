using CodeBase.Logic.UsableObjects.Essentials;
using UnityEngine;

namespace CodeBase.Data.StaticData.Essentials
{
    [CreateAssetMenu(fileName = "Essential Data", menuName = "Static Data/Essentials", order = 0)]
    public class EssentialStaticData : ScriptableObject
    {
        public EssentialType EssentialType;
        public Sprite Sprite;
    }
}