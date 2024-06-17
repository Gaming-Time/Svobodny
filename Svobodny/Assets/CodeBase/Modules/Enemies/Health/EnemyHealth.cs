using System.Collections;
using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Ai.Entity;
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
        [SerializeField] private GameObject collider;

        private HumanoidAnimatorController _animatorController;
        private HumanoidAnimationEventsHandler _animationEventsHandler;
        private EnemyVFXController _vfxController;
        private EnemyAudioController _audioController;
        private EnemyAiEntity _entity;
        private WaitForSeconds _waitForHitAnimationTime;

        public int Health => _currentHealth;

        public void Construct(HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler, EnemyVFXController vfxController,
            EnemyAudioController audioController, EnemyAiEntity entity, int health)
        {
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _vfxController = vfxController;
            _audioController = audioController;
            _entity = entity;

            _animationEventsHandler.ExitDeathAnimationEvent += DestroyAfterDeath;
            _animationEventsHandler.FallAnimationEvent += HandleFall;
            _currentHealth = health;
            _waitForHitAnimationTime = new WaitForSeconds(0.5f);
        }

        private void OnDestroy()
        {
            _animationEventsHandler.ExitDeathAnimationEvent -= DestroyAfterDeath;
            _animationEventsHandler.FallAnimationEvent -= HandleFall;
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
            _entity.IsBeingHit = true;
            StartCoroutine(WaitForHitAnimation());
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
            _entity.IsDead = true;
            _animatorController.PlayDeathAnimation();
        }

        private void HandleFall()
        {
            _audioController.PlayDeath();
            collider.SetActive(false);
        }

        private void DestroyAfterDeath() => Destroy(gameObject, destroyDelay);

        private IEnumerator WaitForHitAnimation()
        {
            yield return _waitForHitAnimationTime;

            _entity.IsBeingHit = false;
            _entity.StartMovement();
        }
    }
}