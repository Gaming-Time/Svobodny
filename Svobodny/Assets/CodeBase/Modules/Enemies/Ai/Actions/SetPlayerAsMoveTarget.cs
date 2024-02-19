using System.Linq;
using Apex.AI;
using CodeBase.Modules.Enemies.Ai.Entity;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    [UsedImplicitly]
    public class SetPlayerAsMoveTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var enemyContext = (EnemyAiContext)context;
            var enemyEntity = enemyContext.Entity;

            var playerEntity = enemyContext.Memory.allObservations
                .First(observation => observation.entity.Type == EntityType.Player).entity;

            enemyEntity.MoveTarget = playerEntity.Position;
        }
    }
}