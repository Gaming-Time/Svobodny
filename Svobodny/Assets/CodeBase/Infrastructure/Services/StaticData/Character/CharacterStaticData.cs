using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.StaticData.Character
{
    [CreateAssetMenu(menuName = "Static Data/Character", fileName = "CharacterData")]
    public class CharacterStaticData : ScriptableObject
    {
        public float WalkSpeed;
        public float SneakSpeed;
    }
}
