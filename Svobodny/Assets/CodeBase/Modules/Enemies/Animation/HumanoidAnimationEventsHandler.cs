using System;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Animation
{
    public class HumanoidAnimationEventsHandler : MonoBehaviour
    {
        public event Action DoDamageAnimationEvent;
        public event Action EnterAttackAnimationEvent;
        public event Action ExitAttackAnimationEvent;
        public event Action EnterDeathAnimationEvent;
        public event Action ExitDeathAnimationEvent;

        public void OnEnterAttackAnimation() => EnterAttackAnimationEvent?.Invoke();
        public void OnExitAttackAnimation() => ExitAttackAnimationEvent?.Invoke();
        public void OnAttackAnimation() => DoDamageAnimationEvent?.Invoke();
        public void OnEnterDeathAnimation() => EnterDeathAnimationEvent?.Invoke();
        public void OnExitDeathAnimation() => ExitDeathAnimationEvent?.Invoke();
    }
}