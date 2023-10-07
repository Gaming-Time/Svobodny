using Assets.CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Modules.Character
{
    public class CharacterMove : MonoBehaviour
    {
        private IInputService _inputService;
        private CharacterController _characterController;

        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float sneakSpeed;

        public void Construct(IInputService inputService, CharacterController characterController)
        {
            _inputService = inputService;
            _characterController = characterController;
        }

        public void Init(float walkSpeed, float sneakSpeed)
        {
            (this.walkSpeed, this.sneakSpeed) = (walkSpeed, sneakSpeed);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 move = new(_inputService.MovementInput.x, 0, _inputService.MovementInput.y);
            move *= _inputService.IsSneakButtonDown() ? sneakSpeed : walkSpeed;

            _characterController.SimpleMove(move);
        }
    }
}