using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<float> OnMoveInput { get; set; }

    [field: SerializeField]
    public UnityEvent OnJumpInput { get; set; }

    [field: SerializeField]
    public UnityEvent OnInteractInput { get; set; }

    [field: SerializeField]
    public UnityEvent OnSprintInputDown { get; set; }

    [field: SerializeField]
    public UnityEvent OnSprintInputUp { get; set; }

    private void Start()
    {
        OnInteractInput.AddListener(GameManager.Instance.TriggerInteraction);
    }

    private void Update()
    {
        UpdateMoveInput();
        UpdateJumpInput();
        UpdateInteractInput();
        UpdateSprintInput();
    }

   
    void UpdateMoveInput()
    {
        float movement = Input.GetAxisRaw("Horizontal");

        OnMoveInput?.Invoke(movement);
    }

    void UpdateJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput?.Invoke();
        }
        
    }

    void UpdateInteractInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            OnInteractInput?.Invoke();
        }
    }

    void UpdateSprintInput()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            OnSprintInputDown?.Invoke();
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            OnSprintInputUp?.Invoke();
        }
        
    }
}
