using CodeBase.Infrastructure.Logic.Animations;
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
    }
}