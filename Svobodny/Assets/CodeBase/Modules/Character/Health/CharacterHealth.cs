using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Health
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _currentHealth;

        public int Health => _currentHealth;

        public void Construct(int health)
        {
            _currentHealth = health;
        }
        
        public void DoDamage(int damage)
        {
            Debug.Log("damage");
            _currentHealth -= damage;
            if(_currentHealth < 1)
                Die();
        }

        public void DoDamage(DamageType damageType, int damage)
        {
            DoDamage(damage);
        }

        public void DoDamage(DamageType damageType, int damage, Vector3 direction)
        {
            DoDamage(damage);
        }

        public void Die()
        {
            Debug.Log("Character is dead");
        }
    }
}