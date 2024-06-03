using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Animation;
using CodeBase.Modules.Enemies.Audio;
using CodeBase.Modules.Enemies.VFX;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Health
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _currentHealth;
        [SerializeField] private float destroyDelay = 3f;

        private HumanoidAnimatorController _animatorController;
        private HumanoidAnimationEventsHandler _animationEventsHandler;
        private EnemyVFXController _vfxController;
        private EnemyAudioController _audioController;

        public int Health => _currentHealth;

        public void Construct(HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler, EnemyVFXController vfxController,
            EnemyAudioController audioController, int health)
        {
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _vfxController = vfxController;
            _audioController = audioController;

            _animationEventsHandler.ExitDeathAnimationEvent += DestroyAfterDeath;
            _animationEventsHandler.FallAnimationEvent += PlayFallAudio;
            _currentHealth = health;
        }

        private void OnDestroy()
        {
            _animationEventsHandler.ExitDeathAnimationEvent -= DestroyAfterDeath;
            _animationEventsHandler.FallAnimationEvent -= PlayFallAudio;
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

            var direction = (from - transform.position).normalized;

            _vfxController.PlayBlood(direction);
            DoDamage(damage);
        }

        public void Die()
        {
            _animatorController.PlayDeathAnimation();
        }

        private void PlayFallAudio() => _audioController.PlayDeath();

        private void DestroyAfterDeath() => Destroy(gameObject, destroyDelay);
    }
}