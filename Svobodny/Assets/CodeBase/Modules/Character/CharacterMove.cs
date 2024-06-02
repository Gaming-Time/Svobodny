using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Audio;
using UnityEngine;

namespace CodeBase.Modules.Character
{
    public class CharacterMove : MonoBehaviour
    {
        private IInputService _inputService;
        private CharacterController _characterController;
        private CharacterAudioController _audioController;

        private bool _isStopped;

        [SerializeField] private float walkSpeed;
        [SerializeField] private float sneakSpeed;

        public void Construct(IInputService inputService, CharacterController characterController,
            CharacterAudioController audioController)
        {
            _inputService = inputService;
            _characterController = characterController;
            _audioController = audioController;
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
    }
}