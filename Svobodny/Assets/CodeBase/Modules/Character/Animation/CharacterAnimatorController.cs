using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private float _walkSpeed;
        private float _sneakSpeed;
        private IInputService _inputService;

        private CharacterController _controller;

        private Animator _animator;

        public void Construct(IInputService inputService, Animator animator, CharacterController controller, float walkSpeed, float sneakSpeed)
        {
            _inputService = inputService;
            _animator = animator;
            _controller = controller;
            _walkSpeed = walkSpeed;
            _sneakSpeed = sneakSpeed;
        }
        
        void Update()
        {
            var cameraInput = _inputService.CameraInput;
            _animator.SetFloat(CharacterAnimatorVariables.MouseX, cameraInput.x);
            _animator.SetFloat(CharacterAnimatorVariables.MouseY, cameraInput.y);
            _animator.SetFloat(CharacterAnimatorVariables.Speed, _controller.velocity.sqrMagnitude);
            _animator.SetFloat(CharacterAnimatorVariables.WalkSpeed, _walkSpeed);
            _animator.SetFloat(CharacterAnimatorVariables.SneakSpeed, _sneakSpeed);
            _animator.SetBool(CharacterAnimatorVariables.IsSneaking, _inputService.IsSneakButtonDown());
            _animator.SetFloat(CharacterAnimatorVariables.MovementX, _inputService.MovementInput.x);
            _animator.SetFloat(CharacterAnimatorVariables.MovementY, _inputService.MovementInput.y);
        }
    }
}