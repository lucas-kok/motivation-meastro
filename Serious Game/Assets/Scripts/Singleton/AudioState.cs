using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    public Sound[] sounds;
    private float _volume = 1;
    public bool IsMuted { get; set; } = false;

    public override void Awake()
    {
        base.Awake();

        if (GameObject.FindGameObjectsWithTag("AudioManager").Length >= 1)
        {
            InstantiateSounds();
        }
    }

    public void Play(string name)
    {
        try
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);
            sound.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Sounds are not loaded in due to not starting from main menu scene.");
        }
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