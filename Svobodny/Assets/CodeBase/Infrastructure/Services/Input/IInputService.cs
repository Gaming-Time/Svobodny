using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementInput { get; }
        Vector2 CameraInput { get; }

        bool IsSneakButtonDown();
    }
}
