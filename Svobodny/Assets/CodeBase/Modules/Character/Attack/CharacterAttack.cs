using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Attack
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask attackLayerMask;

        [SerializeField] private Transform attackPoint;

        private CharacterAnimatorController _animatorController;
        private CharacterAnimationEventsHandler _animationEvents;
        private IInputService _inputService;
        private Collider[] _hitCollection = new Collider[5];

        private bool _isBeingHit;
        private float _previousAttackTimeStamp;

        public void Construct(CharacterAnimatorController animatorController, IInputService inputService,
            CharacterAnimationEventsHandler animationEvents)
        {
            _animatorController = animatorController;
            _animationEvents = animationEvents;
            _inputService = inputService;
            
            _animationEvents.EnterHitAnimationEvent += DisableAttack;
            _animationEvents.ExitHitAnimationEvent += AllowAttack;

            _previousAttackTimeStamp = -attackCooldown;
        }


        private void Update()
        {
            if (Time.time < _previousAttackTimeStamp + attackCooldown)
                return;

            if (_isBeingHit)
                return;

            if (_inputService.IsAttackButtonDown())
            {
                _animatorController.PlayAttackAnimation();
                ScanForTargets();
                _previousAttackTimeStamp = Time.time;
            }
        }

        private void OnDestroy()
        {
            _animationEvents.EnterHitAnimationEvent -= DisableAttack;
            _animationEvents.ExitHitAnimationEvent -= AllowAttack;
        }

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

        private void AllowAttack() => _isBeingHit = false;

        private void DisableAttack() => _isBeingHit = true;
    }
}