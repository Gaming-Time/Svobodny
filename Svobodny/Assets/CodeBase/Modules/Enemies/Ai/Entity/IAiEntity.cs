using System.Collections.Generic;
using CodeBase.Logic.UsableObjects.Closet;
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
        public bool IsDead { get; set; }
        List<Vector3> Waypoints { get; }
        int CurrentWaypointIndex { get; set; }
        bool WasPlayerVisiblePreviously { get; set; }
        bool ShouldHandleShot { get; set; }
        Vector3 LastShotPosition { get; }
        bool IsBeingHit { get; set; }
        bool IsAttacking { get; set; }
        bool ShouldHandleWardrobe { get; }
        Wardrobe Wardrobe { get; }

        void MoveTo(Vector3 destination);
        void MeleeAttack(IEntity target);
        void StartMovement();
        void StopMovement();
        void PlayDetectionSound();
        bool IsPathToPositionValid(Vector3 position);
        void PullOut();
    }
}