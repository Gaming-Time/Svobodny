using CodeBase.Data;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Modules.Character
{
    public class CharacterMove : MonoBehaviour, ISavedProgress
    {
        private IInputService _inputService;
        private CharacterController _characterController;

        private bool _isStopped;

        [SerializeField] private float walkSpeed;
        [SerializeField] private float sneakSpeed;

        public void Construct(IInputService inputService, CharacterController characterController)
        {
            _inputService = inputService;
            _characterController = characterController;
        }

        public void Init(float walkSpeed, float sneakSpeed)
        {
            (this.walkSpeed, this.sneakSpeed) = (walkSpeed, sneakSpeed);
        }

        public void Move()
        {
            var inputNormalized = _inputService.MovementInput.normalized;
            Vector3 move = new(inputNormalized.x, 0, inputNormalized.y);

            var sneakInput = _inputService.IsSneakButtonDown();

            move *= sneakInput ? sneakSpeed : walkSpeed;

            _characterController.SimpleMove(move);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentScene() != progress.PositionOnLevel.Level)
                return;

            var savedPosition = progress.PositionOnLevel.Position;
            var savedRotation = progress.PositionOnLevel.Rotation;

            /*_characterController.enabled = false;

            if (savedPosition != null)
                transform.position = savedPosition.AsUnityVector();

            if (savedRotation != null)
                transform.rotation = Quaternion.Euler(savedRotation.AsUnityVector());

            _characterController.enabled = true;*/
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PositionOnLevel = new PositionOnLevel(CurrentScene(),
                transform.position.AsVectorData(), transform.rotation.eulerAngles.AsVectorData());
        }

        private static string CurrentScene() => SceneManager.GetActiveScene().name;
    }
}