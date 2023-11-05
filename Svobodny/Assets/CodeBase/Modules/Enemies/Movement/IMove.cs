using UnityEngine;

namespace CodeBase.Modules.Enemies.Movement
{
    public interface IMove
    {
        Vector3 Velocity { get; }
        void AllowMovement();
        void Stop();
        void MoveToPosition(Vector3 destination);
    }
}