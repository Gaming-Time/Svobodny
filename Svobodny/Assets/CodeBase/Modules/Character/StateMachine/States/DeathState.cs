using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class DeathState : IUpdatableState
    {
        private readonly IWindowService _windowService;
        private readonly CharacterAnimatorController _animatorController;
        private readonly CharacterAnimationEventsHandler _animationEventsHandler;
        private readonly Collider _characterTrigger;

        public DeathState(IWindowService windowService, CharacterAnimatorController animatorController,
            CharacterAnimationEventsHandler animationEventsHandler, Collider characterTrigger)
        {
            _windowService = windowService;
            _animatorController = animatorController;
            _animationEventsHandler = animationEventsHandler;
            _characterTrigger = characterTrigger;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _characterTrigger.enabled = false;
            _animatorController.PlayDeathAnimation();
            _animationEventsHandler.DeathAnimationFinishedEvent += OnDeathAnimationFinished;
        }

        private void OnDeathAnimationFinished()
        {
            _windowService.OpenOrCreateWindow(WindowID.Death);
        }

        public void Update()
        {
        }
    }
}