using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Door
{
    public class Door : UsableObject
    {
        private DoorAnimatorController _animatorController;

        private bool _isOpen;
        
        protected override IInputService InputService { get; set; }

        public void Construct(IInputService inputService, DoorAnimatorController animatorController, bool isOpen)
        {
            InputService = inputService;
            _animatorController = animatorController;
        }
        public override void Use()
        {
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
            }
            else
            {
                _animatorController.PlayOpenAnimation();
            }

            _isOpen = !_isOpen;
        }
    }
}