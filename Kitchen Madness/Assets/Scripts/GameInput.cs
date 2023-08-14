using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputAction playerInputActions;
    private void Awake()
    {
         playerInputActions = new PlayerInputAction();

        playerInputActions.Player.Enable(); // Enabling Action Map . 

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction ?.Invoke(this, EventArgs.Empty); // Checking for subscribers.
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Action Map "Player " Action "Move" 

        inputVector.Normalize();

        return inputVector;
    }    
}
