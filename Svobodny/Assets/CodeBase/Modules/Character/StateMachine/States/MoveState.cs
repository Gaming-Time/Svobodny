using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Ai;
using CodeBase.Modules.Character.Audio;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Inventory;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class MoveState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterMove _characterMove;
        private readonly InventoryHandler _inventoryHandler;
        private readonly IInputService _inputService;
        private readonly CharacterAudioController _audioController;
        private readonly CharacterController _characterController;
        private readonly PlayerEntity _playerEntity;

        public MoveState(CharacterStateMachine stateMachine, CharacterMove characterMove,
            InventoryHandler inventoryHandler, IInputService inputService, CharacterAudioController audioController,
            CharacterController characterController, PlayerEntity playerEntity)
        {
            _stateMachine = stateMachine;
            _characterMove = characterMove;
            _inventoryHandler = inventoryHandler;
            _inputService = inputService;
            _audioController = audioController;
            _characterController = characterController;
            _playerEntity = playerEntity;
        }


        public void Exit()
        {
            _playerEntity.IsSneaking = false;
            _audioController.DeactivateFootSteps();
        }

        public void Enter()
        {
        }

        public void Update()
        {
            if (_inputService.IsAttackButtonDown() && _inventoryHandler.SelectedGun == GunType.Knife)
            {
                _stateMachine.Enter<MeleeAttackState>();
                return;
            }

            if (_inputService.IsAimButtonHeld() && _inventoryHandler.SelectedGun == GunType.Pistol)
            {
                _stateMachine.Enter<ShootState>();
                return;
            }

            _characterMove.Move();

            var controllerSpeed = _characterController.velocity.sqrMagnitude;
            var sneakInput = _inputService.IsSneakButtonHeld();
            _playerEntity.IsSneaking = sneakInput;

            if (controllerSpeed > 0.01f)
            {
                if (sneakInput)
                    _audioController.ActivateSlowFootsteps();
                else
                    _audioController.ActivateFootSteps();
            }
            else
            {
                _audioController.DeactivateFootSteps();
            }
        }
    }
}