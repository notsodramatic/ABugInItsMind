using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler
{
    public void LoadScene(string sceneName)
    {
        SceneLoader.Instance.TransitionToScene(sceneName);
    }

    public void PlayButtonClickSound()
    {
        FindObjectOfType<UISound>().PlayButtonClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        FindObjectOfType<UISound>().PlayButtonHoverSound();
    }
}
