using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class PullOut : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((EnemyAiContext)context).Entity.PullOut();
        }
    }
}