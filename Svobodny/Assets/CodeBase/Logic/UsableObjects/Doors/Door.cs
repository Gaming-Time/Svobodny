using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Inventory;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class Door : UsableObject
    {
        protected override IInputService InputService { get; set; }
        protected override IWindowService WindowService { get; set; }

        private DoorAnimatorController _animatorController;
        private DoorAudioController _audioController;
        
        private bool _isOpen;
        
        public void Construct(IInputService inputService, IWindowService windowService,
            DoorAnimatorController animatorController, DoorAudioController audioController)
        {
            InputService = inputService;
            WindowService = windowService;
            _animatorController = animatorController;
            _audioController = audioController;
        }

        protected override void Use()
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