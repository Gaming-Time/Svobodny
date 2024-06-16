using System;
using CodeBase.Logic.UsableObjects.Closet;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.EnemyDetection
{
    public interface IEnemyDetectionService : IService
    {
        event Action<Vector3> ShotEvent;
        void RegisterShot(Vector3 position);
        void RegisterWardrobeEnter(Wardrobe wardrobe);
        void RegisterWardrobeExit();
        event Action<Wardrobe> WardrobeEnterEvent;
        event Action WardrobeExitEvent;
    }

    public class EnemyDetectionService : IEnemyDetectionService
    {
        public event Action<Vector3> ShotEvent;
        public event Action<Wardrobe> WardrobeEnterEvent;
        public event Action WardrobeExitEvent;

        public void RegisterShot(Vector3 position)
        {
            ShotEvent?.Invoke(position);
        }

        public void RegisterWardrobeEnter(Wardrobe wardrobe)
        {
            WardrobeEnterEvent?.Invoke(wardrobe);
        }

        public void RegisterWardrobeExit()
        {
            WardrobeExitEvent?.Invoke();
        }
    }
}