using CodeBase.Infrastructure.Logic.Animations;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Door
{
    public class DoorAnimatorController : MonoBehaviour
    {
        private Animator _animator;

        public void Construct(Animator animator) => _animator = animator;

        public void PlayOpenAnimation() => _animator.SetTrigger(AnimatorVariables.Door.OpenTrigger);

        public void PlayCloseAnimation() => _animator.SetTrigger(AnimatorVariables.Door.CloseTrigger);
    }
}