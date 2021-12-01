using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        SetMoveMultiplier(1f);
    }

    public void SetMove(float val)
    {
        anim.SetFloat("move", val);
    }

    public void SetJump(bool val)
    {
        anim.SetBool("jump", val);
    }

    public void TriggerBugging()
    {
        anim.SetTrigger("bugging");
        anim.ResetTrigger("sensible");
        anim.ResetTrigger("normal");
    }
    public void TriggerSensible()
    {
        anim.SetTrigger("sensible");
        anim.ResetTrigger("bugging");
        anim.ResetTrigger("normal");
    }
    public void TriggerNormal()
    {
        anim.SetTrigger("normal");
        anim.ResetTrigger("sensible");
        anim.ResetTrigger("bugging");
    }

    public void ResetTriggers()
    {
        anim.ResetTrigger("normal");
        anim.ResetTrigger("sensible");
        anim.ResetTrigger("bugging");
    }

    public void SetMoveMultiplier(float val)
    {
        anim.SetFloat("moveMultiplier", val);
    }
}
