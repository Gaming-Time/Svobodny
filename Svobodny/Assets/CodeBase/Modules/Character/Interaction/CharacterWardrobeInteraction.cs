using System;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.StateMachine;
using UnityEngine;

namespace CodeBase.Modules.Character.Interaction
{
    public class CharacterWardrobeInteraction : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private IWindowService _windowService;
        private CharacterAnimatorController _animatorController;
        private CharacterAnimationEventsHandler _animationEventsHandler;
        private CharacterController _characterController;
        private CharacterMove _characterMove;
        private CharacterStateMachine _stateMachine;


        public void Construct(IWindowService windowService, CharacterAnimatorController animatorController,
            CharacterAnimationEventsHandler animationEventsHandler,
            CharacterController characterController,
            CharacterMove characterMove)
        {
            _windowService = windowService;
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _characterController = characterController;
            _characterMove = characterMove;
            _stateMachine = GetComponent<CharacterStateMachine>();

            _animationEventsHandler.PullOutAnimationFinishedEvent += OnPulledOut;
        }

        private void OnDestroy()
        {
            if (_animationEventsHandler)
                _animationEventsHandler.PullOutAnimationFinishedEvent -= OnPulledOut;
        }

        private void OnPulledOut() => _windowService.OpenOrCreateWindow(WindowID.Death);

        public void Enter(Vector3 at)
        {
            _stateMachine.enabled = false;
            _characterMove.enabled = false;
            _characterController.enabled = false;
            transform.position = at;
            _animatorController.EnterWardrobe();
        }

        public void PullOut(Vector3 at)
        {
            spriteRenderer.enabled = true;
            transform.position = at;
            _animatorController.PullOut();
        }

        public void Exit()
        {
            spriteRenderer.enabled = true;
            _animatorController.ExitWardrobe();
        }

        public void OnEnterAnimationFinished()
        {
            spriteRenderer.enabled = false;
        }

        public void OnExitAnimationFinished()
        {
            _stateMachine.enabled = true;
            _characterController.enabled = true;
            _characterMove.enabled = true;
        }
    }
}