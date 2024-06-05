using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.StateMachine;
using CodeBase.Modules.Character.StateMachine.States;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Character.VFX;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Health
{
    public class CharacterHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        [SerializeField] private int _currentHealth;

        private CharacterAnimatorController _animatorController;
        private IWindowService _windowService;
        private CharacterVFXController _vfxController;
        private HealthHandler _healthHandler;
        private CharacterStateMachine _stateMachine;

        public int Health => _currentHealth;

        public void Construct(CharacterAnimatorController animatorController, IWindowService windowService,
            CharacterVFXController vfxController, HealthHandler healthHandler,
            CharacterStateMachine characterStateMachine, int health)
        {
            _animatorController = animatorController;
            _windowService = windowService;
            _vfxController = vfxController;
            _healthHandler = healthHandler;
            _stateMachine = characterStateMachine;

            _currentHealth = health;
            _healthHandler.HandleHealthChange(_currentHealth);
        }

        public void DoDamage(int damage)
        {
            _animatorController.Damage();

            _currentHealth -= damage;
            _healthHandler.HandleHealthChange(_currentHealth);

            if (_currentHealth >= 1)
            {
                _stateMachine.Enter<HitState>();
                return;
            }

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

        public void LoadProgress(PlayerProgress progress)
        {
            var loadedHealth = progress.State.Health;

            if (loadedHealth > 0)
                _currentHealth = loadedHealth;
            
            _healthHandler.HandleHealthChange(_currentHealth);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.State.Health = _currentHealth;
        }
    }
}