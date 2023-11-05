using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class MoveToMoveTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyContext = (EnemyAiContext)context;
            var enemyEntity = enemyContext.Entity;
            
            enemyEntity.MoveTo(enemyEntity.MoveTarget.Value);
        }
    }
}