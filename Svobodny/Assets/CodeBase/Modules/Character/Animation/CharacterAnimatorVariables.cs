using UnityEngine;

namespace CodeBase.Modules.Character.Animation
{
    public static class CharacterAnimatorVariables
    {
        public static int MouseX = Animator.StringToHash("MouseX");
        public static int MouseY = Animator.StringToHash("MouseY");
        public static int IsSneaking = Animator.StringToHash("Sneak");
        public static int Speed = Animator.StringToHash("Speed");
        public static int WalkSpeed = Animator.StringToHash("WalkSpeed");
        public static int SneakSpeed = Animator.StringToHash("SneakSpeed");
    }
}
