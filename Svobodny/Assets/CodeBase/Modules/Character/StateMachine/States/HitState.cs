using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class HitState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;

        public HitState(CharacterStateMachine stateMachine, CharacterAnimationEventsHandler animationEventsHandler)
        {
            _stateMachine = stateMachine;

            animationEventsHandler.ExitHitAnimationEvent += OnExitAnimationEnded;
        }

        private void OnExitAnimationEnded() => _stateMachine.Enter<MoveState>();

        public void Exit()
        {
        }

        public void Enter()
        {
        }

        public void Update()
        {
        }
    }
}