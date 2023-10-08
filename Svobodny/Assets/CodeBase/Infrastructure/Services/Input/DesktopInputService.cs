using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public class DesktopInputService : AbstractInputService
    {
        public override Vector2 MovementInput => GetMovementInput();

        public override Vector2 CameraInput => GetCameraInput();

        /*protected override Vector2 GetMovementInput()
        {
            var input = base.GetMovementInput();
            Debug.Log(input);
            return input;
        }*/

        protected override Vector2 GetCameraInput()
        {
            Camera camera = null;
            camera ??= Camera.main;
            var mouseVieportPosition = camera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);

            return new Vector2(mouseVieportPosition.x - 0.5f, mouseVieportPosition.y - 0.5f);
        }
    }
}
