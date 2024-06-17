using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Audio;
using CodeBase.Modules.Character.VFX;
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
        private CharacterVFXController _vfxController;
        private CharacterAudioController _audioController;

        public bool HasEnded { get; private set; }

        private Collider[] _hitCollection = new Collider[5];

        public void Construct(CharacterAnimatorController animatorController,
            CharacterAnimationEventsHandler animationEvents, CharacterVFXController vfxController,
            CharacterAudioController audioController)
        {
            _animatorController = animatorController;
            _animationEvents = animationEvents;
            _vfxController = vfxController;
            _audioController = audioController;

            _animationEvents.AttackEvent += OnAttackFrame;
            _animationEvents.ExitAttackAnimationEvent += OnAttackAnimationExit;
        }

        private void OnDestroy()
        {
            if (_animationEvents)
            {
                _animationEvents.AttackEvent -= OnAttackFrame;
                _animationEvents.ExitAttackAnimationEvent -= OnAttackAnimationExit;
            }
        }

        public void Attack()
        {
            HasEnded = false;
            _animatorController.PlayAttackAnimation();
        }

        private void OnAttackFrame()
        {
            _vfxController.PlaySlice();
            _audioController.PlaySlash();
            ScanForTargets();
        }

        private void ScanForTargets()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRadius, _hitCollection,
                attackLayerMask, QueryTriggerInteraction.Collide);
            if (hitCount == 0)
            {
                _audioController.PlaySlash();
                return;
            }
            
            _audioController.PlayMeleeDamage();

            for (int i = 0; i < hitCount; i++)
            {
                var health = _hitCollection[i].GetComponentInParent<IHealth>();
                health.DoDamage(DamageType.Melee, damage, transform.position);
            }
        }

        private void OnAttackAnimationExit()
        {
            HasEnded = true;
        }
    }
}