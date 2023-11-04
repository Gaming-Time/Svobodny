using CodeBase.Modules.Enemies.Ai.Entity;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai
{
    public class CollisionOwner : MonoBehaviour
    {
        public IEntity AiEntity { get; private set; }

        public void Construct(IEntity entity) => AiEntity = entity;
    }
}