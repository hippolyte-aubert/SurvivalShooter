using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.1f, 3)]
    public float pitch = 1f;
    public bool loop = false;
    public bool playOnAwake = false;
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;
}
