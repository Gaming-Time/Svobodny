using Apex.AI;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    [UsedImplicitly]
    public class AttackTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var enemyEntity = enemyContext!.Entity;
            
            enemyEntity.MeleeAttack(enemyEntity.AttackTarget);
        }
    }
}