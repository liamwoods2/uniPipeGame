﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public AudioClip clip;

    public string name;

    [Range (0f, 1f)]
    public float volume;

    [Range (.1f, 5)]
    public float pitch;

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;
}
