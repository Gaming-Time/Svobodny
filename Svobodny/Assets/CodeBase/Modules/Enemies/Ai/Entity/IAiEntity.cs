using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Entity
{
    public interface IAiEntity : IEntity
    {
        Vector3? MoveTarget { get; set; }
        IEntity AttackTarget { get; set; }
        float ScanRange { get; }
        float FovAngle { get; }
        float MeleeAttackRange { get; }
        Vector3 Velocity { get; }
        public bool IsDead { get; }

        void MoveTo(Vector3 destination);
        void MeleeAttack(IEntity target);
        void StartMovement();
        void StopMovement();
    }
}