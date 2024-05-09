using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Attack;
using UnityEngine;

namespace CodeBase.Modules.Character.StateMachine.States
{
    public class ShootState : IUpdatableState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly CharacterRangeAttack _rangeAttack;
        private readonly Transform _arm;
        private  Plane _plane;
        private Vector3 _worldPosition;

        public ShootState(CharacterStateMachine stateMachine, IInputService inputService,
            CharacterRangeAttack rangeAttack, Transform arm)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _rangeAttack = rangeAttack;
            _arm = arm;

            /*_plane = new Plane(Vector3.up, 0);*/
        }

        public void Exit()
        {
            _arm.gameObject.SetActive(false);
        }

        public void Enter()
        {
            _arm.gameObject.SetActive(true);
        }

        public void Update()
        {
            if(!_inputService.IsAimButtonHeld())
                _stateMachine.Enter<MoveState>();
            var mousePosition = _inputService.MousePosition;
            var _camera = Camera.main;
            var ray = _camera.ScreenPointToRay(mousePosition);

            _plane = new Plane(_arm.forward, 0);

            if (_plane.Raycast(ray, out var distance))
            {
                _worldPosition = ray.GetPoint(distance);
            } 

            _worldPosition.z = _arm.position.z;
            
            Debug.Log(_worldPosition);

            var direction = (_worldPosition - _arm.position).normalized;

            _arm.right = direction;
        }
    }
}