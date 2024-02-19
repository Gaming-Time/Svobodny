using System.Linq;
using Apex.AI;
using Apex.Serialization;
using CodeBase.Modules.Enemies.Ai.Entity;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Scorers
{
    public class IsEntityVisibleAndInAttackRange : ContextualScorerBase
    {
        [ApexSerialization] [UsedImplicitly] public bool Not;
        [ApexSerialization] [UsedImplicitly] public EntityType EntityType;
        public override float Score(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var memory = enemyContext!.Memory;
            var entity = enemyContext!.Entity;

            var target = memory.allObservations.FirstOrDefault(observation =>
                observation.entity.Type == EntityType && observation.isVisible);

            if (target == null)
                return Not ? score : 0f;

            if (Vector3.Distance(entity.Position, target.position) > entity.MeleeAttackRange)
                return Not ? score : 0f;

            return Not ? 0f : score;
        }
    }
}