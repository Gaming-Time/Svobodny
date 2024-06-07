using System.Linq;
using Apex.AI;
using Apex.Serialization;
using CodeBase.Modules.Character.Ai;
using CodeBase.Modules.Enemies.Ai.Entity;
using CodeBase.Modules.Enemies.Ai.Helpers;
using CodeBase.Modules.Enemies.Ai.Memory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

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

                var direction = (enemyEntity.Velocity == Vector3.zero)
                    ? enemyEntity.AttackTarget == null
                        ? -enemyEntity.GameObject.transform.forward
                        : (enemyEntity.AttackTarget.Position - enemyEntity.Position).normalized
                    : Vector3.Normalize(enemyEntity.Velocity);

                bool visibility;

                if (hitEntity.Type == EntityType.Player)
                {
                    Assert.IsTrue(hitEntity is PlayerEntity);

                    var playerEntity = (PlayerEntity)hitEntity;

                    visibility = playerEntity.IsSneaking
                        ? Utilities.IsVisibleWithFov(enemyEntity.Position, playerEntity.Position, direction,
                            enemyEntity.ScanRange, enemyEntity.FovAngle, BlockLayers)
                        : Utilities.IsVisible(enemyEntity.Position, playerEntity.Position, enemyEntity.ScanRange,
                            BlockLayers);
                }
                else
                {
                    visibility = Utilities.IsVisible(enemyEntity.Position, hitEntity.Position,
                        enemyEntity.ScanRange, BlockLayers);
                }

                enemyContext.Memory.AddOrUpdateObservation(new Observation(hitEntity, visibility));
            }

            Debug.Log(enemyContext.Memory.allObservations.Where(obs => obs.isVisible).ToList().Count);
        }
    }
}