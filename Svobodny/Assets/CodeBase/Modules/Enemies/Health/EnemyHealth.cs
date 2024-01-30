using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Health
{
    public class EnemyHealth : MonoBehaviour, IHealth
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
            _currentHealth -= damage;
            
            if(damage < 1)
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
            Destroy(gameObject);
        }
    }
}