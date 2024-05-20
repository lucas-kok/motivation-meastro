using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioState : GenericSingleton<AudioState>
{
    private SoundsData _soundsData;
    private float _volume = 1;

    public bool IsMuted { get; set; } = false;

    public override void Awake()
    {
        base.Awake();
        _soundsData = Resources.Load<SoundsData>("SoundsData");

        InstantiateSounds();
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_soundsData.Sounds, sound => sound.name == name);
        sound.source.Play();
    }

    private void InstantiateSounds()
    {
        foreach (Sound sound in _soundsData.Sounds)
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
        foreach (Sound sound in _soundsData.Sounds)
        {
            sound.source.volume = volume;
        }
        _volume = volume;
    }

    public float GetCurrentVolume() => _volume;
}