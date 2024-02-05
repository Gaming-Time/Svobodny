using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Animation;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Closet
{
    public class Wardrobe : UsableObject
    {
        private GameObject _character;
        private WardrobeAnimatorController _animatorController;
        private CharacterAnimatorController _characterAnimatorController;

        private bool _isActive;

        protected override IInputService InputService { get; set; }

        public void Construct(IInputService inputService, GameObject character,
            WardrobeAnimatorController animatorController, CharacterAnimatorController characterAnimatorController)
        {
            InputService = inputService;
            _character = character;
            _animatorController = animatorController;
            _characterAnimatorController = characterAnimatorController;
        }

        protected override void OnUpdate()
        {
            if (!_isActive)
            {
                base.OnUpdate();
                return;
            }

            if (InputService.IsUseButtonDown())
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