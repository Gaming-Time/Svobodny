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

        public static bool IsVisibleWithFov(Vector3 from, Vector3 to, Vector3 forward, float range, float viewAngle,
            LayerMask blockLayers)
        {
            if (range == 0f)
                return false;

            var sqrMag = (to - from).sqrMagnitude;
            if (sqrMag == 0f)
                return true;

            if (sqrMag > range * range)
                return false;

            var direction = (to - from).normalized;

            if (Vector3.Angle(forward, direction) >= viewAngle / 2)
                return false;

            var blockHit = Physics.Linecast(from, to, blockLayers);

            return !blockHit;
        }
    }
}