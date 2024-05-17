using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    public AudioMixer audioMixer;
    public AudioSource audioSource;

    public void SetVolume(float volume) => audioMixer.SetFloat("volume", volume);
}
