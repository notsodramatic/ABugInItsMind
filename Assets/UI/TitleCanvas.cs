using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoBehaviour
{
    public GameObject notificationPanel;
    public Text notificationText;

    private void Start()
    {
        if (GameManager.Instance.newAchievement)
        {
            ShowNotification("New achievement unlocked!");
        }
    }
    public void ShowNotification(string notification)
    {
        HideNotification();
        notificationText.text = notification;
        notificationPanel.SetActive(true);

        Invoke("HideNotification", 2f);
    }

    public void HideNotification()
    {
        if (notificationPanel.activeInHierarchy)
        {
            notificationPanel.SetActive(false);
        }
    }
}
