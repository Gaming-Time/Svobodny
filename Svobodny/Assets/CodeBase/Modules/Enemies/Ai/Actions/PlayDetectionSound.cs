using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class PlayDetectionSound : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;
            enemyEntity.PlayDetectionSound();
        }
    }
}