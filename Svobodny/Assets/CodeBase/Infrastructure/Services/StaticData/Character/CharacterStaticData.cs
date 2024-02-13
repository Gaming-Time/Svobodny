using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData.Character
{
    [CreateAssetMenu(menuName = "Static Data/Character", fileName = "CharacterData")]
    public class CharacterStaticData : ScriptableObject
    {
        [Range(1,100)]
        public int Health;
        public float WalkSpeed;
        public float SneakSpeed;
    }
}
