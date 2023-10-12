using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputAction playerInputActions;
    private void Awake()
    {
         playerInputActions = new PlayerInputAction();

        playerInputActions.Player.Enable(); // Enabling Action Map . 

        playerInputActions.Player.Interact.performed += Interact_performed; // Key E fires off its event . Interact performed is subed so it fires off.
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) // This is return fires off another event .
    {
        OnInteractAction ?.Invoke(this, EventArgs.Empty); // Checking for subscribers. Player class subs to it.
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Action Map "Player " Action "Move" 

        inputVector.Normalize();

        return inputVector;
    }    
}
