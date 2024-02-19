using UnityEngine;

public static class HumanoidAnimationVariables{
    public static int DirectionXHash = Animator.StringToHash("DirectionX");
    public static int DirectionYHash = Animator.StringToHash("DirectionY");
    public static int AttackDirectionXHash = Animator.StringToHash("AttackDirectionX");
    public static int AttackDirectionYHash = Animator.StringToHash("AttackDirectionY");
    public static int SpeedHash = Animator.StringToHash("Speed");
    public static int AttackTriggerHash = Animator.StringToHash("Attack");
    public static int DieTriggerHash = Animator.StringToHash("Die");
}