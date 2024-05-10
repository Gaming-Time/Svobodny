using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Animation;
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

        private Collider[] _hitCollection = new Collider[1];
        private float _lastAttackTime = 0f;

        public void Construct(float attackRange, HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler, EnemyVFXController vfxController)
        {
            overlapRadius = attackRange;
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _vfxController = vfxController;
        }

        public void Attack(Vector3 targetPosition)
        {
            if (Time.time < _lastAttackTime + attackDelay)
                return;

            _animatorController.SetAttackDirection(targetPosition);
            _animatorController.PlayAttackAnimation();
            _vfxController.PlaySlice();
            ScanForTargets();

            _lastAttackTime = Time.time;
        }

        private void ScanForTargets()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, overlapRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Ignore);
            if (hitCount == 0)
                return;

            Debug.Log("hit");
            var hitCollider = _hitCollection[0];

            var health = hitCollider.GetComponentInParent<IHealth>();
            health.DoDamage(DamageType.Melee, damage, attackPoint.position);
        }
    }
}