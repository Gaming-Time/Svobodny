using CodeBase.Data.StaticData.Sound;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Music
{
    public class MusicTrigger : MonoBehaviour
    {
        private IStaticDataService _staticDataService;

        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private LayerMask playerLayers;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            audioSource.clip = _staticDataService.ForSound(soundType).AudioClip;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!playerLayers.IsLayerInMask(other.gameObject.layer))
                return;
            
            audioSource.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if(!playerLayers.IsLayerInMask(other.gameObject.layer))
                return;
            
            audioSource.Stop();
        }
    }
}