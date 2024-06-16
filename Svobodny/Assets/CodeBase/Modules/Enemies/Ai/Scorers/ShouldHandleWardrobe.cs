using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class ShouldHandleWardrobe : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;

            return enemyEntity.ShouldHandleWardrobe ? score : 0f;
        }
    }
}