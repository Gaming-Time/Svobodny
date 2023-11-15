using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class CleanupMemory : ActionBase
    {
        [ApexSerialization,
         FriendlyName("Visibility Expiration Threshold",
             "How many seconds old an observation is before its 'isVisible' is set to false")]
        public float VisibilityExpirationThreshold = 1.5f;

        public override void Execute(IAIContext context)
        {
            var enemyContext = (EnemyAiContext)context;
            var memory = enemyContext.Memory;

            var observations = memory.allObservations;
            if (observations.Count == 0)
                return;

            for (int i = observations.Count - 1; i >= 0; i--)
            {
                var observation = observations[i];
                if (observation.entity == null)
                {
                    memory.RemoveObservationAt(i);
                    return;
                }

                if (Time.time - observation.timestamp >= VisibilityExpirationThreshold)
                    observation.isVisible = false;
            }
        }
    }
}