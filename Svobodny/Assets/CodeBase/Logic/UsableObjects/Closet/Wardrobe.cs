using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
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
        private bool _isPullingOut;
        public Transform EnemyPivot => enemyPivot;

        protected override IInputService InputService { get; set; }
        protected override IWindowService WindowService { get; set; }

        public void Construct(IInputService inputService, IEnemyDetectionService detectionService, IWindowService windowService,
            WardrobeAnimatorController animatorController, CharacterWardrobeInteraction wardrobeInteraction)
        {
            InputService = inputService;
            WindowService = windowService;
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

        protected override void Use()
        {
            base.Use();
            if(_isPullingOut)
                return;
            _isActive = true;
            _animatorController.Enter();
            _characterWardrobeInteraction.Enter(characterPivot.position);
            _detectionService.RegisterWardrobeEnter(this);
        }

        public void PullOut()
        {
            _isPullingOut = true;
            _characterWardrobeInteraction.PullOut(characterPulloutPivot.position);
            _animatorController.PullOut();
        }
        
        public void StartPlayerAnimation() => _characterWardrobeInteraction.Exit();
        public void OnExitAnimationFinished() => _isActive = false;

        private void GetOut()
        {
            if(_isPullingOut)
                return;
            _detectionService.RegisterWardrobeExit();
            _animatorController.Exit();
        }
    }
}