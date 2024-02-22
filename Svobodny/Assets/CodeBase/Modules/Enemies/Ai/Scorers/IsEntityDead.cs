using Apex.AI;
using Apex.Serialization;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsEntityDead : ContextualScorerBase
    {
        [ApexSerialization] public bool Not;

        public override float Score(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var isDead = enemyContext!.Entity.IsDead;

            if (isDead)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}