using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
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

        protected virtual Vector2 GetMovementInput() => 
            new(UnityEngine.Input.GetAxis(HorizontalAxis), UnityEngine.Input.GetAxis(VerticalAxis));

        protected virtual Vector2 GetCameraInput() =>
            new(UnityEngine.Input.GetAxis(CameraHorizontalAxis), UnityEngine.Input.GetAxis(CameraVerticalAxis));

        public virtual bool IsSneakButtonDown() => UnityEngine.Input.GetButton(SneakButton);
    }
}
