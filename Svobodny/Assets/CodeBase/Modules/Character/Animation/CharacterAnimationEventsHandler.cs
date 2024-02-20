using System;
using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        public event Action EnterHitAnimationEvent;
        public event Action ExitHitAnimationEvent;
        
        public void OnEnterHitAnimation()
        {
            EnterHitAnimationEvent?.Invoke();
        }

        public void OnExitHitAnimation()
        {
            ExitHitAnimationEvent?.Invoke();
        }
    }
}