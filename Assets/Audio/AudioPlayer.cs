using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;

    [SerializeField]
    float pitchRandomness = 0.05f;

    float basePitch;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        basePitch = audioSource.pitch;
    }

    protected void PlayClipWithVariablePitch(AudioClip clip)
    {
        var randomPitch = Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    protected void SetVolume(float vol)
    {
        if (audioSource.volume != vol)
        {
            audioSource.volume = vol;
        }
    }
}
