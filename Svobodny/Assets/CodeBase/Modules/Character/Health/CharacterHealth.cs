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
        private CharacterMove _characterMove;

        public int Health => _currentHealth;

        public void Construct(CharacterAnimatorController animatorController, CharacterMove characterMove, int health)
        {
            _animatorController = animatorController;
            _characterMove = characterMove;
            
            _currentHealth = health;
        }
        
        public void DoDamage(int damage)
        {
            _animatorController.Damage();
            _characterMove.StopMovement();
            
            _currentHealth -= damage;
            if (_currentHealth < 1)
            {
                Die();
                
                return;
            }
            
            _characterMove.AllowMovement();
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