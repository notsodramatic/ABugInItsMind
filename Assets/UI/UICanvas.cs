using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public static UICanvas Instance;

    public GameObject mbPrefab, sbPrefab;
    public Transform mbParent, sbParent;

    public GameObject dialoguePanel, choicePanel, actionResultPanel, notificationPanel;

    public Text dialogueText;

    public GameObject choiceBtnPrefab;
    public Transform choicesParent;

    public Text notificationText;

    public Image illustratedImage;
    public Text actionResultText;

    string[] dialogueLines;
    int currentDialogueIndex;

    List<GameObject> newMBs, newSBs;

    bool triggerNPCDeath;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke("InitializeMindStats", 0.5f);
        Invoke("ShowInitialGuide", 0.5f);

        newMBs = new List<GameObject>();
        newSBs = new List<GameObject>();
    }
    void InitializeMindStats()
    {
        UpdateMB(GameManager.Instance.currentBugPoint);
        UpdateSB(GameManager.Instance.currentSensibleBitPoint);
    }

    public void UpdateMB(int mindBugPoint)
    {
        foreach (Image mb in mbParent.GetComponentsInChildren<Image>())
        {
            Destroy(mb.gameObject);
        }
        for (int i = 0; i < mindBugPoint; i++)
        {
            Instantiate(mbPrefab, mbParent, false);
        }
    }
    public void UpdateSB(int sensibleBitPoint)
    {
        foreach (Image sb in sbParent.GetComponentsInChildren<Image>())
        {
            Destroy(sb.gameObject);
        }
        for (int i = 0; i < sensibleBitPoint; i++)
        {
            Instantiate(sbPrefab, sbParent, false);
        }
    }
    public void AddMB(int amount)
    {
        newMBs.Clear();
        newSBs.Clear();
        for (int i = 0; i < amount; i++)
        {
            newMBs.Add(Instantiate(mbPrefab, mbParent, false));
        }
    }
    public void AddSB(int amount)
    {
        newMBs.Clear();
        newSBs.Clear();
        for (int i = 0; i < amount; i++)
        {
            newSBs.Add(Instantiate(sbPrefab, sbParent, false));
        }
    }

    public void SetDialoguePanelActive(bool active)
    {
        dialoguePanel.SetActive(active);
    }
    public void UpdateDialogueText(string[] dialogueTexts)
    {
        currentDialogueIndex = 0;
        dialogueLines = dialogueTexts;
        dialogueText.text = dialogueLines[currentDialogueIndex];
    }
    public void ShowNextDialogueLine()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];
        }
        else
        {
            SetDialoguePanelActive(false);
            SetChoicePanelActive(true);
        }
    }
    public void SetChoicePanelActive(bool active)
    {
        choicePanel.SetActive(active);
    }
    public void UpdateChoices(PlayerChoiceSO[] choices)
    {
        foreach(Button child in choicesParent.GetComponentsInChildren<Button>())
        {
            Destroy(child.gameObject);
        }
        foreach (PlayerChoiceSO choiceSO in choices)
        {
            GameObject choiceBtn = Instantiate(choiceBtnPrefab, choicesParent, false);
            choiceBtn.GetComponentInChildren<Text>().text = choiceSO.choice;
            choiceBtn.GetComponent<Button>().onClick.AddListener(() => {
                OnChoiceBtnClicked(choiceSO);
            });
        }
    }

    void OnChoiceBtnClicked(PlayerChoiceSO choiceSO)
    {
        if (choiceSO.end2)
        {
            GameManager.Instance.ResetMindStats();
            GameManager.Instance.AddAchievement(2);
            SceneLoader.Instance.TransitionToScene("End2");
        } else if (choiceSO.end3)
        {
            GameManager.Instance.ResetMindStats();
            GameManager.Instance.AddAchievement(3);
            SceneLoader.Instance.TransitionToScene("End3");
        } else if (choiceSO.end4Or5)
        {
            if (GameManager.Instance.currentBugPoint + GameManager.Instance.currentSensibleBitPoint >= choiceSO.requiredBugPoint + choiceSO.requiredSensibleBitPoint)
            {
                if (FindObjectOfType<PlayerController>().HasDagger())
                {
                    GameManager.Instance.ResetMindStats();
                    GameManager.Instance.AddAchievement(4);
                    SceneLoader.Instance.TransitionToScene("End4");
                }
                else
                {
                    GameManager.Instance.ResetMindStats();
                    GameManager.Instance.AddAchievement(5);
                    SceneLoader.Instance.TransitionToScene("End5");
                }
            }
            else
            {
                ShowNotification("The voice can't be heard at the moment.");
                SetChoicePanelActive(false);
                GameManager.Instance.EnablePlayerMovement();
            }
            
        } else 
        {
            if (choiceSO.requiredBugPoint != 0)
            {
                if (choiceSO.requiredDagger)
                {
                    if (FindObjectOfType<PlayerController>().HasDagger())
                    {
                        if (GameManager.Instance.currentBugPoint >= choiceSO.requiredBugPoint)
                        {
                            SelectNonEndingChoice(choiceSO);
                        }
                        else
                        {
                            ShowNotification("Its mind is not bugging enough.");
                        }
                    }
                    else
                    {
                        ShowNotification("There's something else in its mind.");
                    }
                }
                else
                {
                    if (GameManager.Instance.currentBugPoint >= choiceSO.requiredBugPoint)
                    {
                        SelectNonEndingChoice(choiceSO);
                    }
                    else
                    {
                        ShowNotification("Its mind is not bugging enough.");
                    }
                }
               
            } else if (choiceSO.requiredSensibleBitPoint != 0)
            {
                if (GameManager.Instance.currentSensibleBitPoint >= choiceSO.requiredSensibleBitPoint)
                {
                    SelectNonEndingChoice(choiceSO);
                }
                else
                {
                    ShowNotification("Its mind is not sensible enough.");
                }
            }
            else
            {
                if (choiceSO.requiredDagger)
                {
                    if (FindObjectOfType<PlayerController>().HasDagger())
                    {
                        SelectNonEndingChoice(choiceSO);
                    }
                    else
                    {
                        ShowNotification("There's something else in its mind.");
                    }
                }
                else
                {
                    SelectNonEndingChoice(choiceSO);
                }
                
            }
        }
    }

    void SelectNonEndingChoice(PlayerChoiceSO choiceSO)
    {
        if (choiceSO.rewardBugPoint != 0)
        {
            GameManager.Instance.currentBugPoint += choiceSO.rewardBugPoint;
            
            AddMB(choiceSO.rewardBugPoint);
        }

        if (choiceSO.rewardSensibleBitPoint != 0)
        {
            GameManager.Instance.currentSensibleBitPoint += choiceSO.rewardSensibleBitPoint;

            AddSB(choiceSO.rewardSensibleBitPoint);
        }

        UpdateResultPanel(choiceSO.illustratedImage, choiceSO.result);
        SetChoicePanelActive(false);
        SetActionResultPanelActive(true);

        if (choiceSO.removeDagger)
        {
            FindObjectOfType<PlayerController>().RemoveDagger();
        }

        if (!choiceSO.destroyTarget)
        {
            GameManager.Instance.currentInteractedTarget.GetComponent<NPC>().DisableTrigger();
        }
        else
        {
            triggerNPCDeath = true;
            GameManager.Instance.currentInteractedTarget.GetComponent<NPC>().DisableTrigger();
        }

        if (choiceSO.soundEffectIfAny != null)
        {
            FindObjectOfType<PlayerAudio>().PlaySound(choiceSO.soundEffectIfAny, choiceSO.soundVolumeIfAny);
        }
    }
   
    public void SetActionResultPanelActive(bool active)
    {
        actionResultPanel.SetActive(active);

        if (active == false)
        {
            GameManager.Instance.EnablePlayerMovement();

            for (int i = 0; i < newMBs.Count; i++)
            {
                newMBs[i].GetComponent<Animation>().Play();
            }
            for (int i = 0; i < newSBs.Count; i++)
            {
                newSBs[i].GetComponent<Animation>().Play();
            }

            if (triggerNPCDeath)
            {
                triggerNPCDeath = false;
                NPC currentNPC = GameManager.Instance.currentInteractedTarget.GetComponent<NPC>();
                currentNPC.TriggerDeath();
            }

            GameObject player = GameObject.FindWithTag("Player");

            if (GameManager.Instance.currentBugPoint > GameManager.Instance.currentSensibleBitPoint)
            {
                if (!player.GetComponent<PlayerController>().BuggingBodyColor())
                {
                    player.GetComponent<PlayerAnim>().TriggerBugging();
                }
                
            } else if (GameManager.Instance.currentSensibleBitPoint > GameManager.Instance.currentBugPoint)
            {
                if (!player.GetComponent<PlayerController>().SensibleBodyColor())
                {
                    player.GetComponent<PlayerAnim>().TriggerSensible();
                }
            }
            else
            {
                if (!player.GetComponent<PlayerController>().NormalBodyColor())
                {
                    player.GetComponent<PlayerAnim>().TriggerNormal();
                }
            }
        }
    }
    public void UpdateResultPanel(Sprite image, string text)
    {
        illustratedImage.sprite = image;
        actionResultText.text = text;
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

    void ShowInitialGuide()
    {
        HideNotification();
        notificationText.text = "Press A/D to walk. Hold LEFT SHIFT to run. Press SPACEBAR to jump";
        notificationPanel.SetActive(true);

        Invoke("HideNotification", 5f);
    }
}
