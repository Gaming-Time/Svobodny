using Apex.AI;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    [UsedImplicitly]
    public class AllowMovement : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            enemyContext!.Entity.StartMovement();
        }
    }
}