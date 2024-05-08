using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class DesktopInputService : AbstractInputService
    {
        public override Vector2 MovementInput => GetMovementInput();

        public override Vector2 CameraInput => GetCameraInput();
        public override Vector3 MousePosition => GetMousePosition();
        public override float ScrollInput => GetScrollInput();


        protected override Vector2 GetCameraInput()
        {
            var camera = Camera.main;
            var mouseViewportPosition = UnityEngine.Input.mousePosition;

            return new Vector2(mouseViewportPosition.x - Screen.width / 2f,
                mouseViewportPosition.y - Screen.height / 2f).normalized;
        }
    }
}