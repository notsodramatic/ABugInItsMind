using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldExit : MonoBehaviour
{
    public string exitToScene;
    public bool triggerEnding;
    public int endNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (triggerEnding)
            {
                GameManager.Instance.ResetMindStats();
                GameManager.Instance.AddAchievement(endNumber);
                SceneLoader.Instance.TransitionToScene("End"+endNumber);
            }
            else
            {
                GameManager.Instance.ResetMindStats();
                SceneLoader.Instance.TransitionToScene(exitToScene);
            }
        }
    }
}
