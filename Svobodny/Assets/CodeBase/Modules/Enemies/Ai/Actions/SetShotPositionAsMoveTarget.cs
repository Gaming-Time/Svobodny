using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class SetShotPositionAsMoveTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyAiEntity = ((EnemyAiContext)context).Entity;
            enemyAiEntity.MoveTarget = enemyAiEntity.LastShotPosition;
        }
    }
}