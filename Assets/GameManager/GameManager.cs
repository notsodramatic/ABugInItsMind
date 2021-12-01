using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool triggerNPC, triggerObject, pickUpDagger;

    public string[] currentDialogueLines;
    public PlayerChoiceSO[] currentChoices;
    public GameObject currentInteractedTarget;

    public int initialBugPoint, initialSensibleBitPoint;
    public int currentBugPoint, currentSensibleBitPoint;
    //public int totalMindPoint = 100;

    public string[] achievements;
    public List<int> currentAchievements;
    public bool newAchievement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ResetMindStats();

        currentAchievements = new List<int>();

        for (int i = 0; i < achievements.Length; i++)
        {
            string key = "A" + (i);
            if (PlayerPrefs.HasKey(key))
            {
                if (PlayerPrefs.GetInt(key) == 1)
                {
                    currentAchievements.Add(i);
                }
            }
        }
    }

    public void TriggerInteraction()
    {
        if (triggerNPC)
        {
            triggerNPC = false;
            TriggerDialogue(); 
        } else if (triggerObject)
        {
            triggerObject = false;
            TriggerChoices();
        } else if (pickUpDagger)
        {
            pickUpDagger = false;
            FindObjectOfType<PlayerController>().AddDagger();
            Destroy(FindObjectOfType<Dagger>().gameObject);
        }
    }
    void TriggerDialogue()
    {
        DisablePlayerMovement();

        UICanvas.Instance.HideNotification();

        UICanvas.Instance.UpdateDialogueText(currentDialogueLines);
        UICanvas.Instance.SetDialoguePanelActive(true);
        UICanvas.Instance.UpdateChoices(currentChoices);

        FindObjectOfType<UISound>().PlayButtonClickSound();
    }
    void TriggerChoices()
    {
        DisablePlayerMovement();

        UICanvas.Instance.HideNotification();

        UICanvas.Instance.UpdateChoices(currentChoices);
        UICanvas.Instance.SetChoicePanelActive(true);

        FindObjectOfType<UISound>().PlayButtonClickSound();
    }

    public void DisablePlayerMovement()
    {
        FindObjectOfType<PlayerController>().SetCanMove(false);
    }
    public void EnablePlayerMovement()
    {
        FindObjectOfType<PlayerController>().SetCanMove(true);
    }

    public void AddAchievement(int id)
    {
        if (!currentAchievements.Contains(id))
        {
            currentAchievements.Add(id);
            newAchievement = true;

            PlayerPrefs.SetInt(("A" + id), 1);
            PlayerPrefs.Save();
        }
        else
        {
            newAchievement = false;
        }
    }
    public void ResetMindStats()
    {
        currentBugPoint = initialBugPoint;
        currentSensibleBitPoint = initialSensibleBitPoint;
    }
}
