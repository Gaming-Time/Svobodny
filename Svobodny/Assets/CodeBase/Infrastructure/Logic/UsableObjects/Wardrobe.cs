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

        public override void Use()
        {
            if (!_isActive)
            {
                _character.SetActive(false);
                _isActive = true;
            }
            else
            {
                _character.SetActive(true);
                _isActive = false;
            }
        }
    }
}