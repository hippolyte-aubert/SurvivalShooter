using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            GameObject soundGameObject = new GameObject("Sound_" + s.name);
            soundGameObject.transform.SetParent(this.transform);
            s.source = soundGameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public void Play(string sound)
    {
        Sound s = System.Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();

    }

    public void Stop(string sound)
    {

        Sound s = System.Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();

    }

    public void PlayOneShot(string sound)
    {
        Sound s = System.Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.PlayOneShot(s.clip);
    }

    public AudioSource SoundAudioSource(string name)
    {
        Sound s = System.Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        return s.source;
    }

    public Sound GetSound(string name)
    {
        Sound s = System.Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        return s;
    }

}
