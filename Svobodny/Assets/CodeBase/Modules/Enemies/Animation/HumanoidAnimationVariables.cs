using UnityEngine;

namespace CodeBase.Modules.Enemies.Animation
{
    public static class HumanoidAnimationVariables
    {
        public static int DirectionXHash = Animator.StringToHash("DirectionX");
        public static int DirectionYHash = Animator.StringToHash("DirectionY");
        public static int AttackDirectionXHash = Animator.StringToHash("AttackDirectionX");
        public static int AttackDirectionYHash = Animator.StringToHash("AttackDirectionY");
        public static int SpeedHash = Animator.StringToHash("Speed");
        public static int AttackTriggerHash = Animator.StringToHash("Attack");
        public static int DieTriggerHash = Animator.StringToHash("Die");
        public static int HitTriggerHash = Animator.StringToHash("Hit");
        public static int HitDirectionXHash = Animator.StringToHash("HitDirectionX");
        public static int HitDirectionYHash = Animator.StringToHash("HitDirectionY");
    }
}