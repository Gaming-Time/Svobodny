using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class DesktopInputService : AbstractInputService
    {
        public override Vector2 MovementInput => GetMovementInput();

        public override Vector2 CameraInput => GetCameraInput();
        public override Vector3 MousePosition => GetMousePosition();

        protected override Vector2 GetCameraInput()
        {
            var camera = Camera.main;
            var mouseViewportPosition = camera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);

            return new Vector2(mouseViewportPosition.x - 0.5f, mouseViewportPosition.y - 0.5f);
        }
    }
}
