using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class AimState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterAnimatorController _animatorController;

        public AimState(CharacterStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}