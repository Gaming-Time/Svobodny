using UnityEngine;

namespace CodeBase.Infrastructure.Logic.Animations
{
    public static class AnimatorVariables
    {
        public static class Character
        {
            public static class Movement
            {
                public static readonly int MouseX = Animator.StringToHash("MouseX");
                public static readonly int MouseY = Animator.StringToHash("MouseY");
                public static readonly int IsSneaking = Animator.StringToHash("Sneak");
                public static readonly int Speed = Animator.StringToHash("Speed");
                public static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");
                public static readonly int SneakSpeed = Animator.StringToHash("SneakSpeed");
                public static readonly int MovementX = Animator.StringToHash("MovementX");
                public static readonly int MovementY = Animator.StringToHash("MovementY");
            }

            public static class Interactions
            {
                public static readonly int EnterWardrobe = Animator.StringToHash("EnterWardrobe");
                public static readonly int ExitWardrobe = Animator.StringToHash("ExitWardrobe");
            }

            public static class Battle
            {
                public static readonly int HitTriggerHash = Animator.StringToHash("Hit");
                public static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
            }
        }

        public static class Wardrobe
        {
            public static readonly int EnterTrigger = Animator.StringToHash("Enter");
            public static readonly int ExitTrigger = Animator.StringToHash("Exit");
        }

        public static class Door
        {
            public static readonly int OpenTrigger = Animator.StringToHash("Open");
            public static readonly int CloseTrigger = Animator.StringToHash("Close");
        }
    }
}