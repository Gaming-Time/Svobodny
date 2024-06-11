using Apex.AI;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    [UsedImplicitly]
    public class SelectClosestWaypoint : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            var minDistance = float.MaxValue;
            int index = 0;

            for (int i = 0; i < enemyEntity.Waypoints.Count; i++)
            {
                var waypoint = enemyEntity.Waypoints[i];
                var distance = Vector3.Distance(enemyEntity.Position, waypoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }

            enemyEntity.CurrentWaypointIndex = index;
        }
    }
}