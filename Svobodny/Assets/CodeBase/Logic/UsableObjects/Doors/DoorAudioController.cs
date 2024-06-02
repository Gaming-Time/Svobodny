using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Doors
{
    public class DoorAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        private IStaticDataService _staticDataService;
        private AudioClip _openSound;
        private AudioClip _closeSound;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            _openSound = _staticDataService.ForSound(SoundType.OpenDoor).AudioClip;
            _closeSound = _staticDataService.ForSound(SoundType.CloseDoor).AudioClip;
        }

        public void PlayOpenSound()
        {
            audioSource.clip = _openSound;
            audioSource.Play();
        }

        public void PlayCloseSound()
        {
            audioSource.clip = _closeSound;
            audioSource.Play();
        }
    }
}