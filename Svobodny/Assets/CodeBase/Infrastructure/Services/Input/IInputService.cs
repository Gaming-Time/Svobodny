using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementInput { get; }
        Vector2 CameraInput { get; }
        Vector3 MousePosition { get; }
        float ScrollInput { get; }

        bool IsSneakButtonDown();
        bool IsUseButtonDown();
        bool IsAttackButtonDown();
        bool IsAimButtonDown();
        bool IsAimButtonUp();
        bool IsAimButtonHeld();
        bool IsKnifeSlotSelectedButtonDown();
        bool IsPistolSLotSelectedButtonDown();
    }
}
