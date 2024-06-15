using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Modules.Enemies.Attack;
using CodeBase.Modules.Enemies.Audio;
using CodeBase.Modules.Enemies.Health;
using CodeBase.Modules.Enemies.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Modules.Enemies.Ai.Entity
{
    public class EnemyAiEntity : MonoBehaviour, IAiEntity
    {
        private IMove _mover;
        private EnemyAttack _attacker;
        private EnemyHealth _enemyHealth;
        private EnemyAudioController _audioController;
        private IEnemyDetectionService _enemyDetectionService;

        [SerializeField] private float scanRange;
        [SerializeField] private float meleeAttackRange;
        [SerializeField] private float fovAngle;

        public EntityType Type => EntityType.Enemy;
        public GameObject GameObject => gameObject;
        public Vector3 Position => transform.position;
        public Vector3? MoveTarget { get; set; }
        public IEntity AttackTarget { get; set; }
        public float ScanRange => scanRange;
        public float FovAngle => fovAngle;
        public float MeleeAttackRange => meleeAttackRange;
        public Vector3 Velocity => _mover.Velocity;
        public bool IsDead { get; set; }
        public List<Vector3> Waypoints { get; private set; }

        public int CurrentWaypointIndex { get; set; }
        public bool WasPlayerVisiblePreviously { get; set; }
        
        public bool ShouldHandleShot { get; set; }
        public Vector3 LastShotPosition { get; private set; }
        public bool IsBeingHit { get; set; }
        
        public bool IsAttacking { get; set; }

        public void Construct(IEnemyDetectionService enemyDetectionService, IMove mover, EnemyAttack attacker,
            EnemyHealth enemyHealth, EnemyAudioController audioController, float scanRange,
            float meleeAttackRange, List<Vector3> waypoints)
        {
            _enemyDetectionService = enemyDetectionService;
            _mover = mover;
            _attacker = attacker;
            _enemyHealth = enemyHealth;
            _audioController = audioController;

            this.scanRange = scanRange;
            this.meleeAttackRange = meleeAttackRange;
            Waypoints = waypoints;
            CurrentWaypointIndex = -1;
            WasPlayerVisiblePreviously = false;
            _enemyDetectionService.ShotEvent += OnShot;
        }

        private void OnDestroy()
        {
            _enemyDetectionService.ShotEvent -= OnShot;
        }

        private void OnShot(Vector3 position)
        {
            LastShotPosition = position;
            ShouldHandleShot = true;
        }

        public void MoveTo(Vector3 destination)
        {
            _mover.MoveToPosition(destination);
        }

        public void MeleeAttack(IEntity target)
        {
            _attacker.SetDirectionAndPlayAnimation(target.Position);
        }

        public void StartMovement()
        {
            _mover.AllowMovement();
        }

        public void StopMovement()
        {
            _mover.Stop();
        }

        public void PlayDetectionSound() => _audioController.PlayDetection();

        public bool IsPathToPositionValid(Vector3 position) => _mover.IsPathToPositionValid(position);
    }
}