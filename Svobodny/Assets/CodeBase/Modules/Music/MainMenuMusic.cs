using System.Linq;
using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Helpers;
using UnityEngine;

namespace CodeBase.Modules.Music
{
    public class MainMenuMusic : MonoBehaviour
    {
        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioSource audioSource;

        public void Construct()
        {
            var audioClip = Resources.LoadAll<SoundStaticData>(AssetPath.StaticDataPath.Sound)
                .First(data => data.SoundType == soundType).AudioClip;
            audioSource.clip = audioClip;
        }

        public void Play() => audioSource.Play();
    }
}