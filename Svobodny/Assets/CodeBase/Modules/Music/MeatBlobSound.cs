using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Music
{
    public class MeatBlobSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        public void Construct(IStaticDataService staticDataService)
        {
            audioSource.clip = staticDataService.ForSound(SoundType.Meat).AudioClip;
            
            audioSource.Play();
        }
    }
}