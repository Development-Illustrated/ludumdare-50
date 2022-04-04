using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume = 1f;
    public float continousVolume = .3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float overrideVolume = 1)
    {
        if(overrideVolume != volume)
            audioSource.PlayOneShot(clip, volume);
        else
            audioSource.PlayOneShot(clip, overrideVolume);

    }

    public void PlayContinous(AudioClip clip)
    {
        audioSource.volume = continousVolume;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopPlaying()
    {
        audioSource.volume = volume;
        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.Stop();
    }
}
