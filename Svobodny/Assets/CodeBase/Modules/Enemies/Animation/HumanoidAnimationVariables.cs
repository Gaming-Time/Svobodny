using UnityEngine;

namespace CodeBase.Modules.Enemies.Animation
{
    public static class HumanoidAnimationVariables
    {
        public static readonly int DirectionXHash = Animator.StringToHash("DirectionX");
        public static readonly int DirectionYHash = Animator.StringToHash("DirectionY");
        public static readonly int AttackDirectionXHash = Animator.StringToHash("AttackDirectionX");
        public static readonly int AttackDirectionYHash = Animator.StringToHash("AttackDirectionY");
        public static readonly int AttackAngleHash = Animator.StringToHash("AttackAngle");
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
        public static readonly int DieTriggerHash = Animator.StringToHash("Die");
        public static readonly int HitTriggerHash = Animator.StringToHash("Hit");
        public static readonly int HitDirectionXHash = Animator.StringToHash("HitDirectionX");
        public static readonly int HitDirectionYHash = Animator.StringToHash("HitDirectionY");
    }
}