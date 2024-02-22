using System;
using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        public event Action EnterHitAnimationEvent;
        public event Action ExitHitAnimationEvent;
        public event Action AttackEvent;
        public event Action EnterAttackAnimationEvent;
        public event Action ExitAttackAnimationEvent;

        public void OnEnterHitAnimation() => EnterHitAnimationEvent?.Invoke();
        public void OnExitHitAnimation() => ExitHitAnimationEvent?.Invoke();

        public void OnAttack() => AttackEvent?.Invoke();
        public void OnEnterAttackAnimation() => EnterAttackAnimationEvent?.Invoke();
        public void OnExitAttackAnimation() => ExitAttackAnimationEvent?.Invoke();
    }
}