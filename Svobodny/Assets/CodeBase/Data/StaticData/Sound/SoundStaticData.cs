using UnityEngine;

namespace CodeBase.Data.StaticData.Sound
{
    [CreateAssetMenu(fileName = "Sound Data", menuName = "Static Data/Sound", order = 0)]
    public class SoundStaticData : ScriptableObject
    {
        public SoundType SoundType;
        public AudioClip AudioClip;
    }

    public enum SoundType   
    {
        KnifeDeath,//
        ShovelDeath,//
        DoorBreak,//
        EnemyDetection,//
        StartScreenMusic,//
        MainMenuMusic,
        GameMusic,//
        PlayerWalk,//
        PlayerSlowWalk,
        EnemyWalk,
        OpenDoor,//
        CloseDoor,//
        MeleeAttack,//
        Slash,//
        EnemyFall,//
        Shoot,//
        Meat,
        Kitchen,
        TV,
    }
}