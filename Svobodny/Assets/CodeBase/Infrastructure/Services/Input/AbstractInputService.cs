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

        public abstract Vector2 MovementInput { get; }

        public abstract Vector2 CameraInput { get; }
        public abstract Vector3 MousePosition { get; }

        protected virtual Vector2 GetMovementInput() => 
            new(UnityEngine.Input.GetAxis(HorizontalAxis), UnityEngine.Input.GetAxis(VerticalAxis));

        protected virtual Vector2 GetCameraInput() =>
            new(UnityEngine.Input.GetAxis(CameraHorizontalAxis), UnityEngine.Input.GetAxis(CameraVerticalAxis));

        protected Vector3 GetMousePosition() => UnityEngine.Input.mousePosition;

        public virtual bool IsSneakButtonDown() => UnityEngine.Input.GetButton(SneakButton);
    }
}
