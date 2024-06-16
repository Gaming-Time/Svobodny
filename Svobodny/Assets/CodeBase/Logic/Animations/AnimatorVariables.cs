using UnityEngine;

namespace CodeBase.Logic.Animations
{
    public static class AnimatorVariables
    {
        public static int PullOut = Animator.StringToHash("PullOut");
        public static class Character
        {
            public static readonly int Angle = Animator.StringToHash("Angle");
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
                public static readonly int IsAiming = Animator.StringToHash("IsAiming");
                public static readonly int DieTriggerHash = Animator.StringToHash("Die");
            }

            public static class Inventory
            {
                public static readonly int IsBearArms = Animator.StringToHash("IsBearArms");
                public static readonly int IsKnifeSelected = Animator.StringToHash("IsKnifeSelected");
                public static readonly int IsPistolSelected = Animator.StringToHash("IsPistolSelected");
            }
        }

        public static class Arm
        {
            public static readonly int MouseX = Animator.StringToHash("MouseX");
            public static readonly int MouseY = Animator.StringToHash("MouseY");
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

        public static class UI
        {
            public static class ItemsInventory
            {
                public static readonly int OpenDescriptionTrigger = Animator.StringToHash("OpenDescription");
                public static readonly int CloseDescriptionTrigger = Animator.StringToHash("CloseDescription");
            }
        }
    }
}