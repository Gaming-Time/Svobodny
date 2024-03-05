using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Animation;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Health
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _currentHealth;
        [SerializeField] private float destroyDelay = 3f;

        private HumanoidAnimatorController _animatorController;
        private HumanoidAnimationEventsHandler _animationEventsHandler;

        public int Health => _currentHealth;

        public void Construct(HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler, int health)
        {
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;

            _animationEventsHandler.ExitDeathAnimationEvent += DestroyAfterDeath;
            _currentHealth = health;
        }

        private void OnDestroy()
        {
            _animationEventsHandler.ExitDeathAnimationEvent -= DestroyAfterDeath;
        }

        public void DoDamage(int damage)
        {
            if (_currentHealth < 1)
                return;

            _currentHealth -= damage;


            if (_currentHealth < 1)
            {
                Die();
                return;
            }

            _animatorController.PLayHitAnimation();
        }


        public void DoDamage(DamageType damageType, int damage)
        {
            DoDamage(damage);
        }

        public void DoDamage(DamageType damageType, int damage, Vector3 from)
        {
            _animatorController.SetHitDirection(from);

            DoDamage(damage);
        }

        public void Die() => _animatorController.PlayDeathAnimation();

        private void DestroyAfterDeath() => Destroy(gameObject, destroyDelay);
    }
}