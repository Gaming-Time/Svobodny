using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CodeBase.Modules.UI
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private Slider slider;

        private const string VolumeParameterName = "MasterVolume";

        private void Start()
        {
            var value = PlayerPrefs.GetFloat("Volume", 1);
           
            slider.value = value;
        }

        public void OnValueChanged(float value)
        {
            mixerGroup.audioMixer.SetFloat(VolumeParameterName, Mathf.Log10(value) * 20);

            PlayerPrefs.SetFloat("Volume", value);
            PlayerPrefs.Save();
        }
    }
}