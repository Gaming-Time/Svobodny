using System.Linq;
using CodeBase.Modules.Common.Health;
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

        private Collider[] _hitCollection = new Collider[1];
        private float _lastAttackTime = 0f;

        public void Construct(float attackRange, HumanoidAnimatorController animatorController)
        {
            overlapRadius = attackRange;
            _animatorController = animatorController;
        }

        public void PlayAttackAnimation()
        {
            if(Time.time < _lastAttackTime + attackDelay)
                return;
            
            _animatorController.PlayAttackAnimation();

            _lastAttackTime = Time.time;
        }

        public void Attack()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, overlapRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Ignore);
            if (hitCount==0)
                return;

            var hitCollider = _hitCollection[1];

            var health = hitCollider.GetComponentInParent<IHealth>();
            health.DoDamage(damage);
        }
    }
}