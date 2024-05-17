using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    public Sound[] sounds;
    private float _volume = 1; 

    public override void Awake()
    {
        base.Awake();
        InstantiateSounds();
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound?.source.Play();
    }

    private void InstantiateSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = _volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

    }

    public bool IsMuted { get; set; } = false;

    public void SetVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = volume;
        }
        _volume = volume;
    }

    public float GetCurrentVolume() => _volume;
}
