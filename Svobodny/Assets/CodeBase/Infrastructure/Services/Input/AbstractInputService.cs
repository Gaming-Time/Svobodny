using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class AbstractInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string CameraVerticalAxis = "Mouse Y";
        private const string CameraHorizontalAxis = "Mouse X";
        private const string SneakButton = "Sneak";
        private const string UseButton = "Use";
        private const string AttackButton = "Fire1";
        private const string KnifeSlotSelectedButton = "Knife Slot";
        private const string PistolSlotSelectedButton = "Pistol Slot";
        private const string AimButton = "Aim Button";

        private Camera _mainCamera;

        public abstract Vector2 MovementInput { get; }

        public abstract Vector2 CameraInput { get; }
        public abstract Vector3 MousePosition { get; }
        
        public abstract float ScrollInput { get; }

        protected virtual Vector2 GetMovementInput() => 
            new(UnityEngine.Input.GetAxis(HorizontalAxis), UnityEngine.Input.GetAxis(VerticalAxis));

        protected virtual Vector2 GetCameraInput() =>
            new(UnityEngine.Input.GetAxis(CameraHorizontalAxis), UnityEngine.Input.GetAxis(CameraVerticalAxis));

        protected Vector3 GetMousePosition() => UnityEngine.Input.mousePosition;

        protected float GetScrollInput() => UnityEngine.Input.mouseScrollDelta.y;

        public virtual Vector3 MouseWorldPosition(Vector3 position)
        {
            _mainCamera ??= Camera.main;
            var mousePosition = MousePosition;
            
            var worldMousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x,
                UnityEngine.Input.mousePosition.y,
                Mathf.Abs(position.z - _mainCamera.transform.position.z)));

            return worldMousePosition;
        }

        public virtual bool IsSneakButtonHeld() => UnityEngine.Input.GetButton(SneakButton);
        public virtual bool IsSneakButtonDown() => UnityEngine.Input.GetButtonDown(SneakButton);
        public bool IsUseButtonDown() => UnityEngine.Input.GetButtonDown(UseButton);
        public bool IsAttackButtonDown() => UnityEngine.Input.GetButtonDown(AttackButton);
        public bool IsAimButtonDown() => UnityEngine.Input.GetButtonDown(AimButton);

        public bool IsAimButtonUp() => UnityEngine.Input.GetButtonUp(AimButton);
        public bool IsAimButtonHeld() => UnityEngine.Input.GetButton(AimButton);

        public bool IsKnifeSlotSelectedButtonDown() => UnityEngine.Input.GetButtonDown(KnifeSlotSelectedButton);
        public bool IsPistolSLotSelectedButtonDown() => UnityEngine.Input.GetButtonDown(PistolSlotSelectedButton);

        public bool IsEscapeButtonDown() => UnityEngine.Input.GetButtonDown("Cancel");
    }
}
