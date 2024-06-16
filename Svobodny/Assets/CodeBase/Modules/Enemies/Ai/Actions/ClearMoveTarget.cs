using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class ClearMoveTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var entity = ((EnemyAiContext)context).Entity;

            entity.MoveTarget = null;
        }
    }
}