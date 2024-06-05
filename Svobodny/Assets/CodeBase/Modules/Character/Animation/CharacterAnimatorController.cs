using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic.Animations;
using CodeBase.Modules.Character.UI;
using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private float _walkSpeed;
        private float _sneakSpeed;
        private IInputService _inputService;

        private CharacterController _controller;
        private Camera _camera;

        private Animator _animator;

        public void Construct(IInputService inputService, Animator animator,
            CharacterController controller, Camera camera, float walkSpeed, float sneakSpeed)
        {
            _inputService = inputService;
            _animator = animator;
            _controller = controller;
            _walkSpeed = walkSpeed;
            _sneakSpeed = sneakSpeed;
            _camera = camera;
        }

        private void Update()
        {
            var mousePosition = _inputService.MousePosition;
            var playerScreenPosition = _camera.WorldToScreenPoint(transform.position);
            var direction = (mousePosition - playerScreenPosition).normalized;
            var angle = Vector2.SignedAngle(Vector2.right, direction);

            _animator.SetFloat(AnimatorVariables.Character.Angle, angle);
            _animator.SetFloat(AnimatorVariables.Character.Movement.MouseX, direction.x);
            _animator.SetFloat(AnimatorVariables.Character.Movement.MouseY, direction.y);
            _animator.SetFloat(AnimatorVariables.Character.Movement.Speed, _controller.velocity.sqrMagnitude);
            _animator.SetFloat(AnimatorVariables.Character.Movement.WalkSpeed, _walkSpeed);
            _animator.SetFloat(AnimatorVariables.Character.Movement.SneakSpeed, _sneakSpeed);
            _animator.SetBool(AnimatorVariables.Character.Movement.IsSneaking, _inputService.IsSneakButtonDown());

            _animator.SetFloat(AnimatorVariables.Character.Movement.MovementX, _inputService.MovementInput.x);
            _animator.SetFloat(AnimatorVariables.Character.Movement.MovementY, _inputService.MovementInput.y);
        }

        public void EnterWardrobe() => _animator.SetTrigger(AnimatorVariables.Character.Interactions.EnterWardrobe);
        public void ExitWardrobe() => _animator.SetTrigger(AnimatorVariables.Character.Interactions.ExitWardrobe);
        public void Damage() => _animator.SetTrigger(AnimatorVariables.Character.Battle.HitTriggerHash);
        public void PlayAttackAnimation() => _animator.SetTrigger(AnimatorVariables.Character.Battle.AttackTriggerHash);
        public void PlayDeathAnimation() => _animator.SetTrigger(AnimatorVariables.Character.Battle.DieTriggerHash);

        public void HandleAim(bool state) =>
            _animator.SetBool(AnimatorVariables.Character.Battle.IsAiming, state);

        public void SelectGun(GunType? gun)
        {
            switch (gun)
            {
                case GunType.Knife:
                    SetGunVariable(GunVariableTypes.BearArms, false);
                    SetGunVariable(GunVariableTypes.Pistol, false);
                    SetGunVariable(GunVariableTypes.Knife, true);
                    break;
                case GunType.Pistol:
                    SetGunVariable(GunVariableTypes.BearArms, false);
                    SetGunVariable(GunVariableTypes.Pistol, true);
                    SetGunVariable(GunVariableTypes.Knife, false);
                    break;
                case null:
                    SetGunVariable(GunVariableTypes.BearArms, true);
                    SetGunVariable(GunVariableTypes.Pistol, false);
                    SetGunVariable(GunVariableTypes.Knife, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gun), gun, null);
            }
        }

        private void SetGunVariable(GunVariableTypes gunType, bool value)
        {
            var id = gunType switch
            {
                GunVariableTypes.Knife => AnimatorVariables.Character.Inventory.IsKnifeSelected,
                GunVariableTypes.Pistol => AnimatorVariables.Character.Inventory.IsPistolSelected,
                GunVariableTypes.BearArms => AnimatorVariables.Character.Inventory.IsBearArms,
                _ => throw new ArgumentOutOfRangeException(nameof(gunType), gunType, null)
            };

            _animator.SetBool(id, value);
        }

        private enum GunVariableTypes
        {
            BearArms,
            Knife,
            Pistol
        }
    }
}