using CodeBase.Modules.Enemies.Movement;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Animation
{
    public class HumanoidAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private IMove _mover;

        private Vector3 _direction;
        private float _speed;


        public void Construct(Animator animator, IMove mover)
        {
            _animator = animator;
            _mover = mover;
        }

        private void Update()
        {
            ReadInformation();
            UpdateVariables();
        }

        private void ReadInformation()
        {
            _direction = _mover.Velocity.normalized;
            _speed = _mover.Velocity.magnitude;
        }

        private void UpdateVariables()
        {
            _animator.SetFloat(HumanoidAnimationVariables.DirectionXHash, _direction.x);
            _animator.SetFloat(HumanoidAnimationVariables.DirectionYHash, _direction.z);
            _animator.SetFloat(HumanoidAnimationVariables.SpeedHash, _speed);
        }

        public void SetAttackDirection(Vector3 targetPosition)
        {
            var position = transform.position;

            var deltaX = targetPosition.x - position.x;
            var deltaY = targetPosition.z - position.z;

            _animator.SetFloat(HumanoidAnimationVariables.AttackDirectionXHash, deltaX);
            _animator.SetFloat(HumanoidAnimationVariables.AttackDirectionYHash, deltaY);
        }

        public void PlayAttackAnimation() => _animator.SetTrigger(HumanoidAnimationVariables.AttackTriggerHash);
        public void PlayDeathAnimation() => _animator.SetTrigger(HumanoidAnimationVariables.DieTriggerHash);
        public void PLayHitAnimation() => _animator.SetTrigger(HumanoidAnimationVariables.HitTriggerHash);

        public void SetHitDirection(Vector3 from)
        {
            var position = transform.position;

            var deltaX = from.x - position.x;
            var deltaY = from.z - position.z;

            _animator.SetFloat(HumanoidAnimationVariables.HitDirectionXHash, deltaX);
            _animator.SetFloat(HumanoidAnimationVariables.HitDirectionYHash, deltaY);
        }
    }
}