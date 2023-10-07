using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public class DesktopInputService : AbstractInputService
    {
        public override Vector2 MovementInput => GetMovementInput();

        public override Vector2 CameraInput => GetCameraInput();

        protected override Vector2 GetMovementInput()
        {
            var input = base.GetMovementInput();
            Debug.Log(input);
            return input;
        }
    }
}
