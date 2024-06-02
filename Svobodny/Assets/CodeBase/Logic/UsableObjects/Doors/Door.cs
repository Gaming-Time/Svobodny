using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class Door : UsableObject
    {
        protected override IInputService InputService { get; set; }
        
        private DoorAnimatorController _animatorController;
        private DoorAudioController _audioController;
        
        private bool _isOpen;
        
        public void Construct(IInputService inputService,
            DoorAnimatorController animatorController, DoorAudioController audioController)
        {
            InputService = inputService;
            _animatorController = animatorController;
            _audioController = audioController;
        }

        public override void Use()
        {
            
            if (_isOpen)
            {
                _animatorController.PlayCloseAnimation();
                _audioController.PlayCloseSound();
            }
            else
            {
                _animatorController.PlayOpenAnimation();
                _audioController.PlayOpenSound();
            }

            _isOpen = !_isOpen;
        }
    }
}