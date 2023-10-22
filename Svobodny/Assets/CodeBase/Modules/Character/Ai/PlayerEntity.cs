using CodeBase.Modules.Enemies.Ai.Entity;
using UnityEngine;

namespace CodeBase.Modules.Character.Ai
{
    public class PlayerEntity : MonoBehaviour, IEntity
    {
        public EntityType Type => EntityType.Player;
        public GameObject GameObject => gameObject;
        public Vector3 Position => transform.position;
    }
}