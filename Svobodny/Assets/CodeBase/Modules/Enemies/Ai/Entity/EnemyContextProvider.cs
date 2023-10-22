using System;
using Apex.AI;
using Apex.AI.Components;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Entity
{
    public class EnemyContextProvider : MonoBehaviour, IContextProvider
    {
        private IAIContext _context;
        private IAiEntity _entity;

        public void Construct(IAiEntity entity, Vector3 startPosition)
        {
            _entity = entity;
            _context = new EnemyAiContext(_entity, startPosition);
        }
        
        public IAIContext GetContext(Guid aiId) => _context;
    }
}