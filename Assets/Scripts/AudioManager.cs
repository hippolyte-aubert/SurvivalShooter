using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioMixer audioMixer;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    public Sound[] sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        foreach (Sound s in sounds)
        {
            GameObject soundGameObject = new GameObject("Sound_" + s.name);
            soundGameObject.transform.SetParent(this.transform);
            s.source = soundGameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
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
    
    public void SetSFXVolume(float volume)
    {
        if (volume < -39) volume = -80;
        audioMixer.SetFloat("SFXVolume", volume);
    }
        
    public void SetMusicVolume(float volume)
    {
        if (volume < -39) volume = -80;
        audioMixer.SetFloat("MusicVolume", volume);
    }
        
    public void SetMasterVolume(float volume)
    {
        if (volume < -39) volume = -80;
        audioMixer.SetFloat("MasterVolume", volume);
    }

}
