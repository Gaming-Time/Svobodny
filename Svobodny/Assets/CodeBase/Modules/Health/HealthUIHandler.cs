using CodeBase.Modules.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Modules.Health
{
    public class HealthUIHandler : MonoBehaviour
    {
        private Slider _slider;

        public void Construct(Hud hud)
        {
            _slider = hud.HealthSlider;
        }

        public void SetHealthSliderValue(float value)
        {
            _slider.value = value;
        }
    }
}