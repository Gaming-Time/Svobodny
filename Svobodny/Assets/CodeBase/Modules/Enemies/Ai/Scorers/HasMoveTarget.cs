using Apex.AI;
using Apex.Serialization;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class HasMoveTarget : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;
        
        public override float Score(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var enemyEntity = enemyContext!.Entity;

            if (enemyEntity.MoveTarget.HasValue)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}