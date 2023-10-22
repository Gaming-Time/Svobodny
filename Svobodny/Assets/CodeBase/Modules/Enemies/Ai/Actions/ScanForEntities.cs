using System.Linq;
using Apex.AI;
using Apex.Serialization;
using CodeBase.Modules.Enemies.Ai.Entity;
using CodeBase.Modules.Enemies.Ai.Helpers;
using CodeBase.Modules.Enemies.Ai.Memory;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Actions
{
    public class ScanForEntities : ActionBase
    {
        [ApexSerialization] [UsedImplicitly] public LayerMask TargetLayerMask;
        [ApexSerialization] [UsedImplicitly] public LayerMask BlockLayers;

        public override void Execute(IAIContext context)
        {
            var enemyContext = context as EnemyAiContext;
            var enemyEntity = enemyContext!.Entity;

            var hits = new Collider[10];
            int hitsCount = Physics.OverlapSphereNonAlloc(enemyEntity.Position, enemyEntity.ScanRange, hits,
                TargetLayerMask,
                QueryTriggerInteraction.Collide);

            for (int i = 0; i < hitsCount; i++)
            {
                var hit = hits[i];

                var hitEntity = hit.GetComponent<IEntity>() ?? hit.GetComponent<CollisionOwner>().AiEntity;

                if (hitEntity == null)
                    continue;

                if (ReferenceEquals(hitEntity, enemyEntity))
                    continue;

                var visibility = Utilities.IsVisible(enemyEntity.Position, hitEntity.Position,
                    enemyEntity.ScanRange, BlockLayers);

                enemyContext.Memory.AddOrUpdateObservation(new Observation(hitEntity, visibility));
            }

            Debug.Log(enemyContext.Memory.allObservations.Where(obs => obs.isVisible).ToList().Count);
        }
    }
}