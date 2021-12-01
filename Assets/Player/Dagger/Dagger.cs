using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UICanvas.Instance.ShowNotification("Press E to pick up");
            GameManager.Instance.pickUpDagger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UICanvas.Instance.HideNotification();
            GameManager.Instance.pickUpDagger = false;
        }
    }
}
