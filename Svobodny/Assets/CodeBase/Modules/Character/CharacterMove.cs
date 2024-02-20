using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Animation;
using UnityEngine;

namespace CodeBase.Modules.Character
{
    public class CharacterMove : MonoBehaviour
    {
        private IInputService _inputService;
        private CharacterAnimationEventsHandler _animationEventsHandler;
        private CharacterController _characterController;

        private bool _isStopped;

        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float sneakSpeed;

        public void Construct(IInputService inputService, CharacterController characterController, CharacterAnimationEventsHandler animationEventsHandler)
        {
            _inputService = inputService;
            _characterController = characterController;
            _animationEventsHandler = animationEventsHandler;

            animationEventsHandler.EnterHitAnimationEvent += StopMovement;
            animationEventsHandler.ExitHitAnimationEvent += AllowMovement;
        }

        public void Init(float walkSpeed, float sneakSpeed)
        {
            (this.walkSpeed, this.sneakSpeed) = (walkSpeed, sneakSpeed);
        }


        void Update()
        {
            if(_isStopped)
                return;
            
            var inputNormalized = _inputService.MovementInput.normalized;
            Vector3 move = new(inputNormalized.x, 0, inputNormalized.y);

            move *= _inputService.IsSneakButtonDown() ? sneakSpeed : walkSpeed;

            _characterController.SimpleMove(move);
        }

        private void OnDestroy()
        {
            _animationEventsHandler.EnterHitAnimationEvent -= StopMovement;
            _animationEventsHandler.ExitHitAnimationEvent -= AllowMovement;
        }

        public void StopMovement() => _isStopped = true;
        public void AllowMovement() => _isStopped = false;
    }
}