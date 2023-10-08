using Assets.CodeBase.Infrastructure.Services.Input;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Modules.Character.Animation
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private float _walkSpeed;
        private float _sneakSpeed;
        private IInputService _inputService;

        private CharacterController _controller;

        private Animator _animator;

        public void Contsruct(IInputService inputService, Animator animator, float walkSpeed, float sneakSpeed)
        {
            _inputService = inputService;
            _animator = animator;
            _walkSpeed = walkSpeed;
            _sneakSpeed = sneakSpeed;
        }

        // Use this for initialization
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            var cameraInput = _inputService.CameraInput;
            var movementInputLength = _inputService.MovementInput.sqrMagnitude;
            _animator.SetFloat(CharacterAnimatorVariables.MouseX, cameraInput.x);
            _animator.SetFloat(CharacterAnimatorVariables.MouseY, cameraInput.y);
            _animator.SetFloat(CharacterAnimatorVariables.Speed, _controller.velocity.sqrMagnitude);
            _animator.SetFloat(CharacterAnimatorVariables.WalkSpeed, _walkSpeed);
            _animator.SetFloat(CharacterAnimatorVariables.SneakSpeed, _sneakSpeed);
            _animator.SetBool(CharacterAnimatorVariables.IsSneaking, _inputService.IsSneakButtonDown());
        }
    }
}