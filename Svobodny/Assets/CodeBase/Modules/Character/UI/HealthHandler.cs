using CodeBase.Modules.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.UI
{
    public class HealthHandler : MonoBehaviour
    {
        private int _startHealth;
        private HealthUIHandler _healthUIHandler;

        public void Construct(HealthUIHandler healthUIHandler, int startHealth)
        {
            _healthUIHandler = healthUIHandler;
            _startHealth = startHealth;
        }

        public void HandleHealthChange(int currentHealth)
        {
            var value = (float)currentHealth / _startHealth;
            _healthUIHandler.SetHealthSliderValue(value);
        }
    }
}