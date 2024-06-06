using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Modules.UI
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup mixerGroup;

        private const string _volumeParameterName = "MasterVolume";

        public void OnValueChanged(float value)
        {
            mixerGroup.audioMixer.SetFloat(_volumeParameterName, value);
        }
    }
}