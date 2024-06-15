using Apex.AI;
using Apex.Serialization;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsBeingHit : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;
        public override float Score(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            if (enemyEntity.IsBeingHit)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}