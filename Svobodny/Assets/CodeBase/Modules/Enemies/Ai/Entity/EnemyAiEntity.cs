using CodeBase.Modules.Enemies.Attack;
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

        [SerializeField] private float scanRange;
        [SerializeField] private float meleeAttackRange;
        [SerializeField] private float fovAngle;

        public EntityType Type { get; }
        public GameObject GameObject => gameObject;
        public Vector3 Position => transform.position;
        public Vector3? MoveTarget { get; set; }
        public IEntity AttackTarget { get; set; }
        public float ScanRange => scanRange;
        public float FovAngle => fovAngle;
        public float MeleeAttackRange => meleeAttackRange;
        public Vector3 Velocity => _mover.Velocity;
        public bool IsDead => _enemyHealth.Health <= 0;

        public void Construct(IMove mover, EnemyAttack attacker, EnemyHealth enemyHealth, float scanRange, float meleeAttackRange)
        {
            _mover = mover;
            _attacker = attacker;
            _enemyHealth = enemyHealth;

            this.scanRange = scanRange;
            this.meleeAttackRange = meleeAttackRange;
        }

        public void MoveTo(Vector3 destination)
        {
            _mover.MoveToPosition(destination);
        }

        public void MeleeAttack(IEntity target)
        {
            _attacker.PlayAttackAnimation(target.Position);
        }

        public void StartMovement()
        {
            _mover.AllowMovement();
        }

        public void StopMovement()
        {
            _mover.Stop();
        }
    }
}