using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : AudioPlayer
{
    public AudioClip stepClip, jumpClip;

    public void PlayWalkStepSound()
    {
        PlaySound(stepClip, 0.7f);
    }
    public void PlayJumpSound()
    {
        PlaySound(jumpClip, 0.5f);
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        SetVolume(volume);
        PlayClipWithVariablePitch(clip);
    }
}
