using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.EnemyDetection
{
    public interface IEnemyDetectionService : IService
    {
        event Action<Vector3> ShotEvent;
        void RegisterShot(Vector3 position);
    }

    public class EnemyDetectionService : IEnemyDetectionService
    {
        public event Action<Vector3> ShotEvent;

        public void RegisterShot(Vector3 position)
        {
            ShotEvent?.Invoke(position);
        }
    }
}