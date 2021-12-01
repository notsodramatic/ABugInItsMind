using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public Transform body;
    public Transform feetPoint;
    public string groundLayerName;
    public float groundCheckRadius;
    public GameObject dagger;

    public Light2D playerLight;
    public Color buggingColor, sensibleColor, normalColor;

    public GameObject[] normalSprites, goodSprites, evilSprites;

    public GameObject landingFX;

    public float sprintFactor;

    Rigidbody2D rb;
    PlayerAnim playerAnim;

    bool canMove = true;

    bool jumping = false;

    float defaultMoveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnim>();
    }

    private void Start()
    {
        defaultMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (CheckIfOnGround() && rb.velocity.y < jumpSpeed)
        {
            playerAnim.SetJump(false);
            if (jumping) //landing
            {
                jumping = false;
                landingFX.SetActive(true);
            }
        }
        else
        {
            playerAnim.SetJump(true);
            landingFX.SetActive(false);
        }
    }

    public void MovePlayer(float movementInput)
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            playerAnim.SetMove(0);
        }
        else
        {
            rb.velocity = new Vector2(movementInput * moveSpeed, rb.velocity.y);

            if (rb.velocity.x <= -1)
            {
                body.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x >= 1)
            {
                body.transform.localScale = Vector3.one;
            }

            playerAnim.SetMove(rb.velocity.magnitude);
        }
    }

    public void Jump()
    {
        if (canMove && CheckIfOnGround())
        {
            jumping = true;
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    bool CheckIfOnGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(feetPoint.position, groundCheckRadius, LayerMask.GetMask(groundLayerName));

        if (hit != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCanMove(bool val)
    {
        canMove = val;
       
        if (val)
        {
            playerAnim.ResetTriggers();
        }
    }

    public bool GetCanMove() => canMove;

    public void RemoveDagger()
    {
        dagger.SetActive(false);
    }
    public void AddDagger()
    {
        dagger.SetActive(true);
        dagger.GetComponent<AudioSource>().Play();
    }

    public bool HasDagger()
    {
        return dagger.activeInHierarchy;
    }

    public void ChangeBodyColorBugging()
    {
        playerLight.color = buggingColor;

        SetSprites(normalSprites, false);
        SetSprites(goodSprites, false);
        SetSprites(evilSprites, true);
    }
    public void ChangeBodyColorSensible()
    {
        playerLight.color = sensibleColor;

        SetSprites(normalSprites, false);
        SetSprites(goodSprites, true);
        SetSprites(evilSprites, false);
    }
    public void ChangeBodyColorNormal()
    {
        playerLight.color = normalColor;

        SetSprites(normalSprites, true);
        SetSprites(goodSprites, false);
        SetSprites(evilSprites, false);
    }
    public bool NormalBodyColor()
    {
        return playerLight.color == normalColor;
    }
    public bool BuggingBodyColor()
    {
        return playerLight.color == buggingColor;
    }
    public bool SensibleBodyColor()
    {
        return playerLight.color == sensibleColor;
    }

    void SetSprites(GameObject[] sprites, bool val)
    {
        foreach (GameObject spr in sprites)
        {
            spr.SetActive(val);
        }
    }

    public void SpeedUp()
    {
        playerAnim.SetMoveMultiplier(sprintFactor);
        moveSpeed *= sprintFactor;
    }
    public void ResetMoveSpeed()
    {
        playerAnim.SetMoveMultiplier(1f);
        moveSpeed = defaultMoveSpeed;
    }
}
