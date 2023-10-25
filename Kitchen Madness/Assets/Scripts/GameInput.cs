using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    private PlayerInputAction playerInputActions;

    public enum Bindings
    {
        Move_UP,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        InteractAlt,
        Pause,
    }
    private void Awake()
    {
        Instance = this;


         playerInputActions = new PlayerInputAction();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable(); // Enabling Action Map . 

        playerInputActions.Player.Interact.performed += Interact_performed; // Key E fires off its event . Interact performed is subed so it fires off.
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        
    }

    private void OnDisable()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed; // Key E fires off its event . Interact performed is subed so it fires off.
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
    
    public string GetBindingText(Bindings binding)
    {
        switch (binding)
        {
            default:

            case Bindings.Move_UP:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Bindings.Move_Down:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Bindings.Move_Left:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();

            case Bindings.Move_Right:
                return playerInputActions.Player.Move.bindings[5].ToDisplayString();

            case Bindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case Bindings.InteractAlt:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Bindings.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();



        }
    }

    public void RebindBinding(Bindings binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch(binding)
        {
            default:

            case Bindings.Move_UP:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Bindings.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 5;
                break;
            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractAlt:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        } 

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Enable();
                onActionRebound();
                
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
         
         
    }

       
}
