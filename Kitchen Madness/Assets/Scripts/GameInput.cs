using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerInputActions;
    private void Awake()
    {
         playerInputActions = new PlayerInputAction();

        playerInputActions.Player.Enable(); // Enabling Action Map . 
    }
    public Vector2 GetMovementVectorNormalized()
    {
        //Vector2 inputVector = new Vector2(0, 0);

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Action Map "Player " Action "Move" 

      /*  if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }*/

        inputVector.Normalize();

        return inputVector;
    }    
}
