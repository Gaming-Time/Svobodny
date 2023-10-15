using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementInput { get; }
        Vector2 CameraInput { get; }
        Vector3 MousePosition { get; }

        bool IsSneakButtonDown();
    }
}
