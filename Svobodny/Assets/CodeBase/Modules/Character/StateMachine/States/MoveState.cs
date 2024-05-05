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

        public MoveState(CharacterStateMachine stateMachine, CharacterMove characterMove, InventoryHandler inventoryHandler)
        {
            _stateMachine = stateMachine;
            _characterMove = characterMove;
            _inventoryHandler = inventoryHandler;
        }


        public void Exit()
        {
        }

        public void Enter()
        {
        }

        public void Update()
        {
            _characterMove.Move();
            
            if(!_inputService.IsAttackButtonDown())
                return;

            switch (_inventoryHandler.SelectedGun)
            {
                case GunType.Knife:
                    _stateMachine.Enter<MeleeAttackState>();
                    break;
                case GunType.Pistol:
                    _stateMachine.Enter<ShootState>();
                    break;
                case null:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}