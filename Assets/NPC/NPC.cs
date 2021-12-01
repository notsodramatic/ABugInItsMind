using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Collider2D triggerCollider;

    public DialogueSO dialogueSO;
    public PlayerChoiceSO[] choiceSO;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UICanvas.Instance.ShowNotification("Press E to talk");

            GameManager.Instance.triggerNPC = true;

            GameManager.Instance.currentDialogueLines = dialogueSO.dialogueLines;
            GameManager.Instance.currentChoices = choiceSO;
            GameManager.Instance.currentInteractedTarget = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (UICanvas.Instance.notificationText.text == "Press E to talk")
            {
                UICanvas.Instance.HideNotification();
            }

            GameManager.Instance.triggerNPC = false;
        }
    }

    public void DisableTrigger()
    {
        triggerCollider.enabled = false;
    }

    public void TriggerDeath()
    {
        if (!anim.enabled)
        {
            anim.enabled = true;
        }
        anim.SetTrigger("death");
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
