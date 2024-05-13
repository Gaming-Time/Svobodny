using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Attack;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class MeleeAttackState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterMeleeAttack _characterMeleeAttack;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitUntil _waitUntilAttackEnded;

        public MeleeAttackState(CharacterStateMachine stateMachine, CharacterMeleeAttack characterMeleeAttack,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _characterMeleeAttack = characterMeleeAttack;
            _coroutineRunner = coroutineRunner;

            _waitUntilAttackEnded = new WaitUntil(() => _characterMeleeAttack.HasEnded);
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _characterMeleeAttack.Attack();
            _coroutineRunner.StartCoroutine(WaitForAttackAnimationFinish());
        }

        public void Update()
        {
        }

        private IEnumerator WaitForAttackAnimationFinish()
        {
            yield return _waitUntilAttackEnded;
            _stateMachine.Enter<MoveState>();
        }
    }
}