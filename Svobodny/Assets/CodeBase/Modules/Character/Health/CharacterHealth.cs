using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.UI;
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
        private HealthHandler _healthHandler;

        public int Health => _currentHealth;

        public void Construct(CharacterAnimatorController animatorController, IWindowService windowService,
            CharacterVFXController vfxController, HealthHandler healthHandler, int health)
        {
            _animatorController = animatorController;
            _windowService = windowService;
            _vfxController = vfxController;
            _healthHandler = healthHandler;

            _currentHealth = health;
            _healthHandler.HandleHealthChange(_currentHealth);
        }

        public void DoDamage(int damage)
        {
            _animatorController.Damage();

            _currentHealth -= damage;
            _healthHandler.HandleHealthChange(_currentHealth);

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