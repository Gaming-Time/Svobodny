using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic.Animations;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private float _walkSpeed;
        private float _sneakSpeed;
        private IInputService _inputService;

        private CharacterController _controller;
        private InventoryHandler _inventoryHandler;

        private Animator _animator;

        public void Construct(IInputService inputService, InventoryHandler inventoryHandler, Animator animator,
            CharacterController controller, float walkSpeed, float sneakSpeed)
        {
            _inputService = inputService;
            _inventoryHandler = inventoryHandler;
            _animator = animator;
            _controller = controller;
            _walkSpeed = walkSpeed;
            _sneakSpeed = sneakSpeed;
            
        }

        private void OnDestroy()
        {
        }

        void Update()
        {
            var cameraInput = _inputService.CameraInput;
            _animator.SetFloat(AnimatorVariables.Character.Movement.MouseX, cameraInput.x);
            _animator.SetFloat(AnimatorVariables.Character.Movement.MouseY, cameraInput.y);
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