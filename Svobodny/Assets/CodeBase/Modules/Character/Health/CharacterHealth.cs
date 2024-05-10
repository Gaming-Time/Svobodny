using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.VFX;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Health
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _currentHealth;

        private CharacterAnimatorController _animatorController;
        private IWindowService _windowService;
        private CharacterVFXController _vfxController;

        public int Health => _currentHealth;

        public void Construct(CharacterAnimatorController animatorController, IWindowService windowService,
            CharacterVFXController vfxController, int health)
        {
            _animatorController = animatorController;
            _windowService = windowService;
            _vfxController = vfxController;

            _currentHealth = health;
        }

        public void DoDamage(int damage)
        {
            _animatorController.Damage();

            _currentHealth -= damage;

            if (_currentHealth >= 1)
                return;

            Die();
        }

        public void DoDamage(DamageType damageType, int damage)
        {
            DoDamage(damage);
        }

        public void DoDamage(DamageType damageType, int damage, Vector3 from)
        {
            var direction = (from - transform.position).normalized;
            _vfxController.PlayBlood(direction);
            DoDamage(damage);
        }

        public void Die()
        {
            gameObject.SetActive(false);
            _windowService.OpenOrCreateWindow(WindowID.Death);
        }
    }
}