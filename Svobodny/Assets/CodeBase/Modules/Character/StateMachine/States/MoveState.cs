using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Inventory;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class MoveState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterMove _characterMove;
        private readonly InventoryHandler _inventoryHandler;
        private readonly IInputService _inputService;

        public MoveState(CharacterStateMachine stateMachine, CharacterMove characterMove,
            InventoryHandler inventoryHandler, IInputService inputService)
        {
            _stateMachine = stateMachine;
            _characterMove = characterMove;
            _inventoryHandler = inventoryHandler;
            _inputService = inputService;
        }


        public void Exit()
        {
        }

        public void Enter()
        {
        }

        public void Update()
        {
            if (_inputService.IsAttackButtonDown() && _inventoryHandler.SelectedGun == GunType.Knife)
                _stateMachine.Enter<MeleeAttackState>();

            if (_inputService.IsAimButtonHeld() && _inventoryHandler.SelectedGun == GunType.Pistol)
                _stateMachine.Enter<ShootState>();

            _characterMove.Move();
        }
    }
}