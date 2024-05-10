using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States;
using CodeBase.Modules.Character.Arm;
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
        private readonly ArmAnimatorController _armAnimatorController;
        private readonly Camera _camera;
        private  Plane _plane;
        private Vector3 _worldPosition;

        public ShootState(CharacterStateMachine stateMachine, IInputService inputService,
            CharacterRangeAttack rangeAttack, Transform arm, Camera camera)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _rangeAttack = rangeAttack;
            _arm = arm;
            _camera = camera;

            _armAnimatorController = _arm.GetComponent<ArmAnimatorController>();
        }

        public void Exit()
        {
            _arm.gameObject.SetActive(false);
        }

        public void Enter()
        {
            _arm.gameObject.SetActive(true);
            SetArmPosition();
            _armAnimatorController.SetMouseVariables();
            SetArmRotation();
        }

        public void Update()
        {
            if(!_inputService.IsAimButtonHeld())
                _stateMachine.Enter<MoveState>();
            SetArmPosition();
            _armAnimatorController.SetMouseVariables();
            SetArmRotation();
        }

        private void SetArmRotation()
        {
            var mousePosition = _inputService.MousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);

            _plane = new Plane(_arm.forward, 0);

            if (_plane.Raycast(ray, out var distance))
            {
                _worldPosition = ray.GetPoint(distance);
            } 

            _worldPosition.z = _arm.position.z;
            
            var direction = (_worldPosition - _arm.position).normalized;

            _arm.right = direction;
        }

        private void SetArmPosition()
        {
            var cameraInput = _inputService.CameraInput;
            var angle = Vector2.SignedAngle(Vector2.right, cameraInput);
                
            _arm.localPosition = angle switch
            {
                > -45 and < 45 => new Vector3(0.086f, 0.431f, -0.375f),
                >= 45 and <= 135 => new Vector3(0.139f, 0.08f, 0.252f),
                <= -45 and >= -135 => new Vector3(-0.157f,0.431f,-0.375f),
                < -135 or > 135 => new Vector3(-0.112f, -0.011f, 0.228f),
                _ => _arm.localPosition
            };
        }
    }
}