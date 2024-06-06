using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementInput { get; }
        Vector2 CameraInput { get; }
        float ScrollInput { get; }
        Vector3 MousePosition { get; }

        bool IsSneakButtonDown();
        bool IsUseButtonDown();
        bool IsAttackButtonDown();
        bool IsAimButtonDown();
        bool IsAimButtonUp();
        bool IsAimButtonHeld();
        bool IsKnifeSlotSelectedButtonDown();
        bool IsPistolSLotSelectedButtonDown();
        Vector3 MouseWorldPosition(Vector3 position);
        bool IsEscapeButtonDown();
    }
}
