using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    public AudioMixer audioMixer;

    public bool isMute = false;

    public AudioSource audioSource;
    public void Mute()
    {
        isMute = !isMute;
    }

    public void SetVolume(float volume)
    {
       audioMixer.SetFloat("volume", volume);
    }

}
