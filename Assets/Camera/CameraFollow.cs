using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth;

    Transform player;

    Vector3 offset;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = offset + player.position;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPos.x, smooth * Time.deltaTime),
                                         Mathf.Lerp(transform.position.y, newPos.y, smooth * Time.deltaTime),
                                         transform.position.z);
    }
}
