using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Attack;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class MeleeAttackState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterMeleeAttack _characterMeleeAttack;
        private readonly CharacterAnimationEventsHandler _animationEventsHandler;

        public MeleeAttackState(CharacterStateMachine stateMachine, CharacterMeleeAttack characterMeleeAttack,
            CharacterAnimationEventsHandler animationEventsHandler)
        {
            _stateMachine = stateMachine;
            _characterMeleeAttack = characterMeleeAttack;
            _animationEventsHandler = animationEventsHandler;
        }

        public void Exit() => _animationEventsHandler.ExitAttackAnimationEvent -= OnAttackAnimationFinished;

        public void Enter()
        {
            _characterMeleeAttack.Attack();
            _animationEventsHandler.ExitAttackAnimationEvent += OnAttackAnimationFinished;
        }

        private void OnAttackAnimationFinished() => _stateMachine.Enter<MoveState>();

        public void Update()
        {
        }
    }
}