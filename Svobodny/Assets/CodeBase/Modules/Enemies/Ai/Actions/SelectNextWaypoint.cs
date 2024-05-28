using Apex.AI;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    [UsedImplicitly]
    public class SelectNextWaypoint : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyContext = (EnemyAiContext)context;
            var enemyEntity = enemyContext.Entity;

            enemyEntity.CurrentWaypointIndex = (enemyEntity.CurrentWaypointIndex + 1) % enemyEntity.Waypoints.Count;
        }
    }
}