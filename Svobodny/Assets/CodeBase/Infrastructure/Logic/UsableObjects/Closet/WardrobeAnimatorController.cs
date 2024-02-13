using CodeBase.Infrastructure.Logic.Animations;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Closet
{
    public class WardrobeAnimatorController : MonoBehaviour
    {
        private Animator _animator;

        public void Construct(Animator animator) => _animator = animator;

        public void Enter() => _animator.SetTrigger(AnimatorVariables.Wardrobe.EnterTrigger);

        public void Exit() => _animator.SetTrigger(AnimatorVariables.Wardrobe.ExitTrigger);
    }
}