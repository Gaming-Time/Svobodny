using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Animation;
using CodeBase.Modules.Enemies.Audio;
using CodeBase.Modules.Enemies.VFX;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Attack
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask attackLayerMask;
        [SerializeField] private float overlapRadius;
        [SerializeField] private int damage;
        [SerializeField] private float attackDelay = 3f;

        private HumanoidAnimatorController _animatorController;
        private HumanoidAnimationEventsHandler _animationEventsHandler;
        private EnemyVFXController _vfxController;
        private EnemyAudioController _audioController;

        private Collider[] _hitCollection = new Collider[1];
        private float _lastAttackTime = 0f;

        public void Construct(float attackRange, HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler, EnemyVFXController vfxController,
            EnemyAudioController audioController)
        {
            overlapRadius = attackRange;
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _vfxController = vfxController;
            _audioController = audioController;
        }

        public void SetDirectionAndPlayAnimation(Vector3 targetPosition)
        {
            if (Time.time < _lastAttackTime + attackDelay)
                return;

            _animationEventsHandler.DoDamageAnimationEvent += Attack;

            _animatorController.SetAttackDirection(targetPosition);
            _animatorController.PlayAttackAnimation();

            _lastAttackTime = Time.time;
        }

        private void Attack()
        {
            _vfxController.PlaySlice();
            ScanForTargets();
        }

        private void ScanForTargets()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, overlapRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Ignore);
            if (hitCount == 0)
            {
                _audioController.PlaySlash();
                return;
            }
            
            _audioController.PlayAttackDamage();

            var hitCollider = _hitCollection[0];

            var health = hitCollider.GetComponentInParent<IHealth>();
            health.DoDamage(DamageType.Melee, damage, attackPoint.position);
        }
    }
}