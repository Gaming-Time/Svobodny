using Apex.AI;
using Apex.Serialization;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsNearWaypoint : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;

        public override float Score(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            var distance = (enemyEntity.MoveTarget.Value - enemyEntity.Position).sqrMagnitude;
            
            Debug.LogWarning(distance);

            if ( distance < 0.8f)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}