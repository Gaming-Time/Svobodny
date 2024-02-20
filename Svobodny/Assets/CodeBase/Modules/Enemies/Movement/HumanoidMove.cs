using CodeBase.Modules.Enemies.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Modules.Enemies.Movement
{
    public class HumanoidMove : MonoBehaviour, IMove
    {
        [SerializeField] private float stopDistance = 0.2f;

        private NavMeshAgent _agent;
        private HumanoidAnimationEventsHandler _animationEventsHandler;

        public Vector3 Velocity => _agent.velocity;

        public void Construct(NavMeshAgent agent, HumanoidAnimationEventsHandler animationEventsHandler, float speed)
        {
            _agent = agent;
            _animationEventsHandler = animationEventsHandler;

            _agent.speed = speed;
            _agent.updateRotation = false;
            
            _animationEventsHandler.EnterAttackAnimationEvent += Stop;
            _animationEventsHandler.ExitAttackAnimationEvent += AllowMovement;
        }

        private void OnDestroy()
        {
            _animationEventsHandler.EnterAttackAnimationEvent -= Stop;
            _animationEventsHandler.ExitAttackAnimationEvent -= AllowMovement;
        }

        public void AllowMovement()
        {
            _agent.isStopped = false;
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void MoveToPosition(Vector3 destination)
        {
            if ((destination - transform.position).sqrMagnitude < stopDistance)
                return;

            if (_agent.isOnNavMesh && NavMesh.SamplePosition(destination, out var hit, 0.5f, _agent.areaMask))
                _agent.SetDestination(hit.position);
        }
    }
}