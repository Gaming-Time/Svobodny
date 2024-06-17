using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic.UsableObjects.Essentials;
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
        [SerializeField] private int medicineHealthAddition;

        private IInputService _inputService;
        private CharacterAnimatorController _animatorController;
        private CharacterVFXController _vfxController;
        private HealthHandler _healthHandler;
        private CharacterStateMachine _stateMachine;
        private InventoryHandler _inventoryHandler;
        private int _startHealth;

        public int Health => _currentHealth;

        public void Construct(IInputService inputService, CharacterAnimatorController animatorController,
            CharacterVFXController vfxController, HealthHandler healthHandler,
            CharacterStateMachine characterStateMachine, InventoryHandler inventoryHandler,
            int health)
        {
            _inputService = inputService;
            _animatorController = animatorController;
            _vfxController = vfxController;
            _healthHandler = healthHandler;
            _stateMachine = characterStateMachine;
            _inventoryHandler = inventoryHandler;

            _currentHealth = health;
            _startHealth = health;
            _healthHandler.HandleHealthChange(_currentHealth);
        }

        private void Update()
        {
            if (_inputService.IsUseMedicineButtonDown() && _inventoryHandler.HasEssential(EssentialType.Medicine))
            {
                AddHealth(medicineHealthAddition);
                _inventoryHandler.RemoveEssential(EssentialType.Medicine);
            }
        }

        public void DoDamage(int damage)
        {
            if(_currentHealth < 1)
                return;
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

        private void AddHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, _currentHealth, _startHealth);
            _healthHandler.HandleHealthChange(_currentHealth);
        }

        public void Die()
        {
            _stateMachine.Enter<DeathState>();
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