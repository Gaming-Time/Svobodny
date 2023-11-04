using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Entity
{
    public class EnemyAiEntity : MonoBehaviour, IAiEntity
    {
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

        public void Construct(float scanRange, float meleeAttackRange)
        {
            this.scanRange = scanRange;
            this.meleeAttackRange = meleeAttackRange;
        }
        
        public void MoveTo(Vector3 destination)
        {
            throw new System.NotImplementedException();
        }

        public void MeleeAttack(IEntity target)
        {
            throw new System.NotImplementedException();
        }

        public void StartMovement()
        {
            throw new System.NotImplementedException();
        }

        public void StopMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}