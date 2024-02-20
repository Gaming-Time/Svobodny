using System;
using System.Linq;
using CodeBase.Modules.Common.Health;
using CodeBase.Modules.Enemies.Animation;
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

        private Collider[] _hitCollection = new Collider[1];
        private float _lastAttackTime = 0f;

        public void Construct(float attackRange, HumanoidAnimatorController animatorController,
            HumanoidAnimationEventsHandler animationEventsHandler)
        {
            overlapRadius = attackRange;
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;

            _animationEventsHandler.DoDamageAnimationEvent += Attack;
        }

        private void OnDestroy()
        {
            _animationEventsHandler.DoDamageAnimationEvent -= Attack;
        }

        public void PlayAttackAnimation(Vector3 targetPosition)
        {
            if (Time.time < _lastAttackTime + attackDelay)
                return;

            _animatorController.SetAttackDirection(targetPosition);
            _animatorController.PlayAttackAnimation();

            _lastAttackTime = Time.time;
        }

        private void Attack()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, overlapRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Ignore);
            if (hitCount == 0)
                return;

            Debug.Log("hit");
            var hitCollider = _hitCollection[0];

            var health = hitCollider.GetComponentInParent<IHealth>();
            health.DoDamage(damage);
        }
    }
}