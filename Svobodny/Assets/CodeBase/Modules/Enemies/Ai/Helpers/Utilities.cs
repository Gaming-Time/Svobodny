using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Helpers
{
    public static class Utilities
    {
        public static bool IsVisible(Vector3 from, Vector3 to, float range, LayerMask layers)
        {
            if (range == 0f)
                return false;

            var sqrMag = (to - from).sqrMagnitude;
            if (sqrMag == 0f)
                return true;

            if (sqrMag > range * range)
                return false;

            var blockHit = Physics.Linecast(from, to, layers);

            return !blockHit;
        }
    }
}