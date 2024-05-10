using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic.Animations;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Modules.Character.Arm
{
    public class ArmAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetMouseVariables()
        {
            var cameraInput = _inputService.CameraInput;
            animator.SetFloat(AnimatorVariables.Arm.MouseX, cameraInput.x);
            animator.SetFloat(AnimatorVariables.Arm.MouseY, cameraInput.y);
        }
    }
}