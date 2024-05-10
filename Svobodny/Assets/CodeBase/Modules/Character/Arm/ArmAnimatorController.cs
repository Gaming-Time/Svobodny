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

        private void Update()
        {
            /*var cameraInput = _inputService.CameraInput;

            var angle = Vector2.SignedAngle(Vector2.right, cameraInput);
            
            Debug.Log($"Angle:{angle}, sin:{Mathf.Sin(angle)}");
            
            animator.SetFloat(AnimatorVariables.Arm.MouseX, cameraInput.x);
            animator.SetFloat(AnimatorVariables.Arm.MouseY, cameraInput.y);*/

            /*transform.localPosition = angle switch
            {
                > -45 and < 45 => new Vector3(0.086f, 0.431f, -0.375f),
                >= 45 and <= 135 => new Vector3(0.139f, 0.08f, 0.252f),
                <= -45 and >= -135 => new Vector3(-0.157f,0.431f,-0.375f),
                < -135 or > 135 => new Vector3(-0.112f, -0.011f, 0.228f),
                _ => transform.localPosition
            };*/
        }

        public void SetMouseVariables()
        {
            var cameraInput = _inputService.CameraInput;
            animator.SetFloat(AnimatorVariables.Arm.MouseX, cameraInput.x);
            animator.SetFloat(AnimatorVariables.Arm.MouseY, cameraInput.y);
        }
    }
}