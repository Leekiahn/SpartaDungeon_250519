using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    [Range(0f, 1f)] public float volume;
    public AudioClip forestClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = forestClip;
        audioSource.Play();
    }
}