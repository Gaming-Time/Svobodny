using CodeBase.Logic.Animations;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void OpenDescription() =>
            _animator.SetTrigger(AnimatorVariables.UI.ItemsInventory.OpenDescriptionTrigger);

        public void CloseDescription() =>
            _animator.SetTrigger(AnimatorVariables.UI.ItemsInventory.CloseDescriptionTrigger);
    }
}