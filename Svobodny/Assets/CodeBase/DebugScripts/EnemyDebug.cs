using System;
using CodeBase.Modules.Enemies.Ai.Entity;
using UnityEditor;
using UnityEngine;

namespace CodeBase.DebugScripts
{
    
    public class EnemyDebug : MonoBehaviour
    {
#if UNITY_EDITOR
        
        public EnemyAiEntity EnemyAiEntity;

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            Gizmos.DrawWireSphere(EnemyAiEntity.Position, EnemyAiEntity.ScanRange);

            var forwardDirection = EnemyAiEntity.Velocity.magnitude < 0.1f
                ? -EnemyAiEntity.GameObject.transform.forward
                : EnemyAiEntity.Velocity.normalized;
            var fromForwardToVelocityRotation = Quaternion.FromToRotation(EnemyAiEntity.GameObject.transform.forward,
                forwardDirection);

            var resultRotation = EnemyAiEntity.GameObject.transform.rotation * fromForwardToVelocityRotation;
            var resultRotationEuler = resultRotation.eulerAngles;

            var viewAngle1 = DirectionFromAngle(resultRotationEuler.y, -EnemyAiEntity.FovAngle / 2);
            var viewAngle2 = DirectionFromAngle(resultRotationEuler.y, EnemyAiEntity.FovAngle / 2);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(EnemyAiEntity.Position, EnemyAiEntity.Position + viewAngle1 * EnemyAiEntity.ScanRange);
            Gizmos.DrawLine(EnemyAiEntity.Position, EnemyAiEntity.Position + viewAngle2 * EnemyAiEntity.ScanRange);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angle)
        {
            angle += eulerY;

            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
#endif
    }
}