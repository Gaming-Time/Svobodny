using Apex.AI;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class ResetShotHandling : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            (context as EnemyAiContext)!.Entity.ShouldHandleShot = false;
        }
    }
}