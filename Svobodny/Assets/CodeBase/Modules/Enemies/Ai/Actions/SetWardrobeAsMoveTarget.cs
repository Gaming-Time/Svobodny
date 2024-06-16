using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class SetWardrobeAsMoveTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var entity = ((EnemyAiContext)context).Entity;

            entity.MoveTarget = entity.Wardrobe.EnemyPivot.position;
        }
    }
}