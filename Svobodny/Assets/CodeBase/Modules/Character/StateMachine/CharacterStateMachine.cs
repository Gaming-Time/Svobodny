using System;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Attack;
using CodeBase.Modules.Character.StateMachine.States;
using CodeBase.Modules.Character.UI;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine
{
    public class CharacterStateMachine : MonoBehaviour, ICoroutineRunner
    {
        private Dictionary<Type, IUpdatableState> _states;
        private IUpdatableState _activeState;

        private IInputService _inputService;
        private InventoryHandler _inventoryHandler;
        private CharacterMove _characterMove;
        private CharacterMeleeAttack _characterMeleeAttack;
        private CharacterAnimationEventsHandler _animationEventsHandler;
        private CharacterRangeAttack _rangeAttack;
        private Camera _camera;
        private CharacterAnimatorController _characterAnimatorController;

        [SerializeField] private Transform arm;

        public void Construct(IInputService inputService, CharacterMove characterMove, CharacterMeleeAttack meleeAttack,
            CharacterAnimationEventsHandler animationEventsHandler, InventoryHandler inventoryHandler,
            CharacterRangeAttack rangeAttack, CharacterAnimatorController characterAnimatorController, Camera camera)
        {
            _inputService = inputService;
            _characterMove = characterMove;
            _characterMeleeAttack = meleeAttack;
            _animationEventsHandler = animationEventsHandler;
            _inventoryHandler = inventoryHandler;
            _rangeAttack = rangeAttack;
            _characterAnimatorController = characterAnimatorController;
            _camera = camera;

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
                [typeof(MoveState)] = new MoveState(this, _characterMove, _inventoryHandler, _inputService),
                [typeof(MeleeAttackState)] = new MeleeAttackState(this, _characterMeleeAttack, this),
                [typeof(ShootState)] = new ShootState(this, _inputService, _rangeAttack, arm, _camera, transform,
                    _characterAnimatorController),
                [typeof(HitState)] = new HitState(this, _animationEventsHandler)
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