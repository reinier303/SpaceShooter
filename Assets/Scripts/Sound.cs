using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    public float Volume = 1;
    public float Pitch = 1;

    [HideInInspector]
    public AudioSource Source;
}
