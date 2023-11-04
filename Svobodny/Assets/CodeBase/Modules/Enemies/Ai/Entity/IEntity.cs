using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Entity
{
    public interface IEntity
    {
        EntityType Type { get; }
        GameObject GameObject { get; }
        Vector3 Position { get; }
    }
}