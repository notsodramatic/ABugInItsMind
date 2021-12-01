using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Choice", menuName ="Player/New Player Choice")]
public class PlayerChoiceSO: ScriptableObject
{
    public string choice;
    public string result;
    public Sprite illustratedImage;
    public int rewardBugPoint, rewardSensibleBitPoint;
    public int requiredBugPoint, requiredSensibleBitPoint;
    public bool end2, end3, end4Or5, removeDagger, destroyTarget, requiredDagger;
    public AudioClip soundEffectIfAny;
    public float soundVolumeIfAny;
}
