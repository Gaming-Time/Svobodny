using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Audio
{
    public class EnemyAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource walkSource;
        [SerializeField] private AudioSource detectionSource;
        [SerializeField] private AudioSource attackSource;

        private IStaticDataService _staticDataService;

        private AudioClip _walkClip;
        private AudioClip _detectionClip;
        private AudioClip _slashClip;
        private AudioClip _attackDamageClip;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            _walkClip = _staticDataService.ForSound(SoundType.EnemyWalk).AudioClip;
            _detectionClip = _staticDataService.ForSound(SoundType.EnemyDetection).AudioClip;
            _slashClip = _staticDataService.ForSound(SoundType.MeleeAttack).AudioClip;
            _attackDamageClip = _staticDataService.ForSound(SoundType.Slash).AudioClip;
            walkSource.clip = _walkClip;
            detectionSource.clip = _detectionClip;
        }

        public void PlayDetection() => detectionSource.Play();

        public void PlaySlash()
        {
            attackSource.clip = _slashClip;
            attackSource.Play();
        }

        public void PlayAttackDamage()
        {
            attackSource.clip = _attackDamageClip;
            attackSource.Play();
        }

        public void ActivateFootsteps()
        {
            if(walkSource.isPlaying)
                return;
            
            walkSource.Play();
        }

        public void DeactivateFootsteps()
        {
            StopWalkSource();
        }

        private void StopWalkSource()
        {       
            if(!walkSource.isPlaying)
                return;
            walkSource.Stop();
        }
    }
}