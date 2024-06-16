using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Interaction;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Closet
{
    public class Wardrobe : UsableObject
    {
        [SerializeField] private Transform characterPivot;
        [SerializeField] private Transform characterPulloutPivot;
        [SerializeField] private Transform enemyPivot;

        private WardrobeAnimatorController _animatorController;
        private CharacterWardrobeInteraction _characterWardrobeInteraction;
        private IEnemyDetectionService _detectionService;

        private bool _isActive;
        public Transform EnemyPivot => enemyPivot;

        protected override IInputService InputService { get; set; }

        public void Construct(IInputService inputService, IEnemyDetectionService detectionService,
            WardrobeAnimatorController animatorController, CharacterWardrobeInteraction wardrobeInteraction)
        {
            InputService = inputService;
            _detectionService = detectionService;
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
            _detectionService.RegisterWardrobeEnter(this);
        }

        public void PullOut()
        {
            _characterWardrobeInteraction.PullOut(characterPulloutPivot.position);
            _animatorController.PullOut();
        }
        
        public void StartPlayerAnimation() => _characterWardrobeInteraction.Exit();
        public void OnExitAnimationFinished() => _isActive = false;

        private void GetOut()
        {
            _detectionService.RegisterWardrobeExit();
            _animatorController.Exit();
        }
    }
}