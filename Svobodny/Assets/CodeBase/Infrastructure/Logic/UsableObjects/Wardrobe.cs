using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects
{
    public class Wardrobe : UsableObject
    {
        private GameObject _character;
        private bool _isActive;

        protected override IInputService InputService { get; set; }

        public void Construct(IInputService inputService, GameObject character)
        {
            InputService = inputService;
            _character = character;
        }

        protected override void OnUpdate()
        {
            if (!_isActive)
            {
                base.OnUpdate();
                return;
            }
            
            if(InputService.IsUseButtonDown())
                GetOut();
        }

        public override void Use()
        {
            _character.SetActive(false);
            _isActive = true;
        }

        public void GetOut()
        {
            _character.SetActive(true);
            _isActive = false;
        }
    }
}