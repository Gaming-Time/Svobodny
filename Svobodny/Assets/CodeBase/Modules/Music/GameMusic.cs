using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Music
{
    public class GameMusic : MonoBehaviour
    {
        private IStaticDataService _staticDataService;

        [SerializeField] private AudioSource source;
        [SerializeField] private SoundType soundType;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            var clip = _staticDataService.ForSound(soundType).AudioClip;
            source.clip = clip;
        }

        public void Play() => source.Play();
    }
}