using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public AudioSource source;
}
