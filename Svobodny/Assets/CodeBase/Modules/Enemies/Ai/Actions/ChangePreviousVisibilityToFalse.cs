using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class ChangePreviousVisibilityToFalse : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyEntity = ((EnemyAiContext)context).Entity;
            enemyEntity.WasPlayerVisiblePreviously = false;
        }
    }
}