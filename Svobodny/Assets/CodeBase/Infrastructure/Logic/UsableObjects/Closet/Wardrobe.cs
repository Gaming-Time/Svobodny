using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.Interaction;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Closet
{
    public class Wardrobe : UsableObject
    {
        [SerializeField] private Transform characterPivot;
        
        private GameObject _character;
        private WardrobeAnimatorController _animatorController;
        private CharacterWardrobeInteraction _characterWardrobeInteraction;

        private bool _isActive;

        protected override IInputService InputService { get; set; }

        public void Construct(IInputService inputService, GameObject character,
            WardrobeAnimatorController animatorController, CharacterWardrobeInteraction wardrobeInteraction)
        {
            InputService = inputService;
            _character = character;
            _animatorController = animatorController;
            _characterWardrobeInteraction = wardrobeInteraction;
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
            _isActive = true;
            _animatorController.Enter();
            _characterWardrobeInteraction.Enter(characterPivot.position);
        }

        public void GetOut()
        {
            _isActive = false;
            _animatorController.Exit();
        }

        public void OnExitAnimationFinished() => _characterWardrobeInteraction.Exit();
    }
}