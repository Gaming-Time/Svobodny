using System;
using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Music
{
    public class MusicTrigger : MonoBehaviour
    {
        private IStaticDataService _staticDataService;

        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioSource audioSource;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            audioSource.clip = _staticDataService.ForSound(soundType).AudioClip;
        }

        private void OnTriggerEnter(Collider other)
        {
            audioSource.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            audioSource.Stop();
        }
    }
}