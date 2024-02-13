using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects.Closet
{
    public class WardrobeAnimationEventsManager : MonoBehaviour
    {
        private Wardrobe _wardrobe;

        public void Construct(Wardrobe wardrobe)
        {
            _wardrobe = wardrobe;
        }

        public void OnExitAnimationFinished() => _wardrobe.OnExitAnimationFinished();
    }
}