using CodeBase.Data.StaticData.Sound;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Modules.Character.Audio
{
    public class CharacterAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource meleeAttackSource;
        [SerializeField] private AudioSource shootSource;
        [SerializeField] private AudioSource walkSource;

        private IStaticDataService _staticDataService;

        private AudioClip _slashClip;
        private AudioClip _meleeDamageClip;
        private AudioClip _shootClip;
        private AudioClip _walkClip;
        private AudioClip _slowWalkClip;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            _slashClip = _staticDataService.ForSound(SoundType.MeleeAttack).AudioClip;
            _meleeDamageClip = _staticDataService.ForSound(SoundType.Slash).AudioClip;
            _shootClip = _staticDataService.ForSound(SoundType.Shoot).AudioClip;
            _walkClip = _staticDataService.ForSound(SoundType.PlayerWalk).AudioClip;
            _slowWalkClip = _staticDataService.ForSound(SoundType.PlayerSlowWalk).AudioClip;

            meleeAttackSource.clip = _slashClip;
            shootSource.clip = _shootClip;
            walkSource.clip = _walkClip;
        }

        public void PlaySlash()
        {
            meleeAttackSource.clip = _slashClip;
            meleeAttackSource.Play();
        }

        public void PlayMeleeDamage()
        {
            meleeAttackSource.clip = _meleeDamageClip;
            meleeAttackSource.Play();
        }

        public void PlayShoot() => shootSource.Play();

        public void ActivateFootSteps()
        {
            if(Equals(walkSource.clip, _walkClip))
                return;
            StopWalkSource();
            walkSource.clip = _walkClip;
            walkSource.Play();
        }

        public void ActivateSlowFootsteps()
        {
            if(Equals(walkSource.clip, _slowWalkClip))
                return;
            StopWalkSource();
            walkSource.clip = _slowWalkClip;
            walkSource.Play();
        }

        public void DeactivateFootSteps() => StopWalkSource();

        private void StopWalkSource()
        {
            if (!walkSource.isPlaying)
                return;
            walkSource.Stop();
            walkSource.clip = null;
        }
    }
}