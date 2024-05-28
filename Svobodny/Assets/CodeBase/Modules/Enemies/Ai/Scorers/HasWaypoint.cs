using Apex.AI;
using Apex.Serialization;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class HasWaypoint : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;

        public override float Score(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            if (enemyEntity.CurrentWaypointIndex >= 0)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}