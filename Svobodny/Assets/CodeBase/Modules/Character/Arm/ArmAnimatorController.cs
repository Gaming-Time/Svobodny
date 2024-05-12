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
        private Transform _characterTransform;
        private Camera _camera;

        public void Construct(IInputService inputService, Camera camera, Transform characterTransform)
        {
            _inputService = inputService;
            _camera = camera;
            _characterTransform = characterTransform;
        }

        public void SetMouseVariables()
        {
            var mouseInput = _inputService.MousePosition;
            var playerScreenPosition = _camera.WorldToScreenPoint(_characterTransform.position);
            var direction = (mouseInput - playerScreenPosition).normalized;
            animator.SetFloat(AnimatorVariables.Arm.MouseX, direction.x);
            animator.SetFloat(AnimatorVariables.Arm.MouseY, direction.y);
        }
    }
}