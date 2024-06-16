using Apex.AI;
using Apex.Serialization;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class HasAttackTarget : ContextualScorerBase
    {
        [ApexSerialization] public bool Not;
        public override float Score(IAIContext context)
        {
            var entity = ((EnemyAiContext)context).Entity;

            var hasTarget = entity.AttackTarget != null;

            if (hasTarget)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}