using Apex.AI;
using CodeBase.Modules.Enemies.Ai.Entity;
using CodeBase.Modules.Enemies.Ai.Memory;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai
{
    public class EnemyAiContext : IAIContext
    {
        public IAiEntity Entity { get; private set; }
        public AiMemory Memory { get; private set; }
        public Vector3 StartPosition { get; private set; }

        public EnemyAiContext(IAiEntity entity, Vector3 startPosition)
        {
            Entity = entity;
            StartPosition = startPosition;
            Memory = new AiMemory();
        }
    }
}