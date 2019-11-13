using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] Sounds;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if(s == null)
        {
            Debug.Log("No sound found with name: " + name);
            return;
        }
        else
        {
            s.Source.Play();
        }
    }
}
