using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class ShouldHandleShot : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            var enemyAIEntity = ((EnemyAiContext)context).Entity;

            if (!enemyAIEntity.ShouldHandleShot)
                return 0f;

            if (enemyAIEntity.IsPathToPositionValid(enemyAIEntity.LastShotPosition)) return score;
            enemyAIEntity.ShouldHandleShot = false;
            return 0f;
        }
    }
}