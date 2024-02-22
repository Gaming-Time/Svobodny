using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Health
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _currentHealth;

        private CharacterAnimatorController _animatorController;

        public int Health => _currentHealth;

        public void Construct(CharacterAnimatorController animatorController, int health)
        {
            _animatorController = animatorController;
            
            _currentHealth = health;
        }
        
        public void DoDamage(int damage)
        {
            _animatorController.Damage();
            
            _currentHealth -= damage;
            
            if (_currentHealth >= 1) 
                return;
            
            PlayDeathAnimation();
        }

        public void DoDamage(DamageType damageType, int damage)
        {
            DoDamage(damage);
        }

        public void DoDamage(DamageType damageType, int damage, Vector3 direction)
        {
            DoDamage(damage);
        }

        public void PlayDeathAnimation()
        {
            Debug.Log("Character is dead");
        }
    }
}