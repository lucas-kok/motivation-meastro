using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public bool IsMuted { get; set; } = false;


    public void SetVolume(float volume) => audioMixer.SetFloat("volume", volume);

    public float GetCurrentVolume() => audioMixer.GetFloat("volume", out var v) ? v : 0;
}
