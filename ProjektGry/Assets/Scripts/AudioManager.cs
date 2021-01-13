using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.volume = sound.volume;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.loop = sound.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Play();
    }

    public void Stop()
    {
        foreach (var sound in sounds)
        {
            sound.audioSource.Stop();
        }
    }
}
