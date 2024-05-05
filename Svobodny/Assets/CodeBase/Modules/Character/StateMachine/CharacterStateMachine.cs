using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Attack;
using CodeBase.Modules.Character.StateMachine.States;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine
{
    public class CharacterStateMachine : MonoBehaviour
    {
        private Dictionary<Type, IUpdatableState> _states;
        private IUpdatableState _activeState;

        private InventoryHandler _inventoryHandler;
        private CharacterMove _characterMove;
        private CharacterMeleeAttack _characterMeleeAttack;
        private CharacterAnimationEventsHandler _animationEventsHandler;

        public void Construct()
        {
            InitializeStateMachine();
            Enter<MoveState>();
        }

        private void Update() => _activeState?.Update();


        public void Enter<TState>() where TState : class, IUpdatableState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private void InitializeStateMachine()
        {
            _states = new Dictionary<Type, IUpdatableState>()
            {
                [typeof(MoveState)] = new MoveState(this, _characterMove, _inventoryHandler),
                [typeof(MeleeAttackState)] = new MeleeAttackState(this, _characterMeleeAttack, _animationEventsHandler),
                [typeof(ShootState)] = new ShootState(),
            };
        }

        private TState ChangeState<TState>() where TState : class, IUpdatableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IUpdatableState =>
            _states[typeof(TState)] as TState;
    }
}