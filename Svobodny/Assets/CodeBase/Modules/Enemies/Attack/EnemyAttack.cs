using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Attack
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask attackLayerMask;
        [SerializeField] private float overlapRadius;

        public void Construct(float attackRange) => overlapRadius = attackRange;

        public void Attack(int damage)
        {
            var hitCollider = Physics2D.OverlapCircle(attackPoint.position, overlapRadius, attackLayerMask);
            if(hitCollider == null)
                return;

            var health = hitCollider.GetComponentInParent<IHealth>();
            health.DoDamage(damage);
        }
    }
}