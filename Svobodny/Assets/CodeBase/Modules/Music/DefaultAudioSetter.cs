using System;
using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Modules.Music
{
    public class DefaultAudioSetter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup mixer;

        private void Start()
        {
            var value = PlayerPrefs.GetFloat("Volume", 1);
            mixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }
    }
}