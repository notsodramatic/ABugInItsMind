using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public Text[] achievementTexts;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.achievements.Length; i++)
        {
            UpdateAchievement(i);
        }
    }

    public void UpdateAchievement(int id)
    {
        string key = "A" + (id);
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) == 1)
            {
                achievementTexts[id].text = (id) + ". " + GameManager.Instance.achievements[id];
                GameManager.Instance.newAchievement = false;
            }
            else
            {
                achievementTexts[id].text = (id) + ". ?????????";
            }
        }
        else
        {
            achievementTexts[id].text = (id) + ". ?????????";
        }
    }

}
