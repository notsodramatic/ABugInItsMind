using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : AudioPlayer
{
    public AudioClip buttonClickClip, buttonHoverClip;

    public void PlayButtonClickSound()
    {
        SetVolume(0.2f);
        PlayClipWithVariablePitch(buttonClickClip);
    }

    public void PlayButtonHoverSound()
    {
        SetVolume(0.7f);
        PlayClipWithVariablePitch(buttonHoverClip);
    }
}
