using Apex.AI;
using Apex.Serialization;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsNearMoveTarget : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;

        public override float Score(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            if (enemyEntity.MoveTarget == null)
                return Not ? score : 0f;

            var distance = (enemyEntity.MoveTarget.Value - enemyEntity.Position).sqrMagnitude;

            if ( distance < 0.9f)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}