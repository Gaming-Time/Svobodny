using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Attack
{
    public class CharacterMeleeAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask attackLayerMask;

        [SerializeField] private Transform attackPoint;

        private CharacterAnimatorController _animatorController;
        private CharacterAnimationEventsHandler _animationEvents;
        private Collider[] _hitCollection = new Collider[5];

        public void Construct(CharacterAnimatorController animatorController,
            CharacterAnimationEventsHandler animationEvents)
        {
            _animatorController = animatorController;
            _animationEvents = animationEvents;

            _animationEvents.AttackEvent += ScanForTargets;
        }

        private void OnDestroy()
        {
            if (_animationEvents)
                _animationEvents.AttackEvent -= ScanForTargets;
        }

        public void Attack() => _animatorController.PlayAttackAnimation();

        private void ScanForTargets()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Collide);
            if (hitCount == 0)
                return;

            for (int i = 0; i < hitCount; i++)
            {
                var health = _hitCollection[i].GetComponentInParent<IHealth>();
                health.DoDamage(DamageType.Melee, damage, transform.position);
            }
        }
    }
}