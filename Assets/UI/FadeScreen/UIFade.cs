using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public static UIFade Instance;
    Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        anim.SetTrigger("fadeOut");
    }

    public void FadeIn()
    {
        anim.SetTrigger("fadeIn");
    }
}
