using System.Linq;
using Apex.AI;
using Apex.Serialization;
using CodeBase.Modules.Enemies.Ai.Entity;
using JetBrains.Annotations;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsEntityVisibleAndInScanRange : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;
        
        [ApexSerialization] [UsedImplicitly] public EntityType EntityType;
        
        public override float Score(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var memory = enemyContext!.Memory;

            var target = memory.allObservations.FirstOrDefault(observation =>
                observation.entity.Type == EntityType && observation.isVisible);

            if (target != null)
                return Not ? 0f : score;

            return Not ? score : 0f;
        }
    }
}