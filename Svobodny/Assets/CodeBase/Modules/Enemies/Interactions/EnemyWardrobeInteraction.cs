using CodeBase.Logic.UsableObjects.Closet;
using CodeBase.Modules.Enemies.Animation;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Interactions
{
    public class EnemyWardrobeInteraction : MonoBehaviour
    {
        private HumanoidAnimatorController _animatorController;
        private bool _isPullingOut;

        public void Construct(HumanoidAnimatorController animatorController) =>
            _animatorController = animatorController;
        
        public void PullOut(Wardrobe wardrobe)
        {
            if(_isPullingOut)
                return;

            _isPullingOut = true;
            transform.position = wardrobe.EnemyPivot.position;
            wardrobe.PullOut();
            _animatorController.PlayPullOutAnimation();
        }
    }
}