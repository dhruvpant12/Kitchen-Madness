using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private ClearCounter selectedCounter;

    private bool isWalking;
    private Vector3 lastInteractionDir;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("more than 1 instance of player class");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {

        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
        
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();

    }

    private void HandleMovement()
    {
         bool canMove;
         Vector3 moveDir;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f; // Height of player used for Capsule cast
        float playerHeight = 2f; // Radios of player used for Capsule cast

        //Checking for collision , should return true for movement.
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //Checking if movement is allowed during wall hugging.
        if (!canMove)
        { 
            //Check mvoement along x axis.
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; // Normalised to not change speed.
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can only move along X axis 
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move along X axis . Checking for Z axis.
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized; // Normalised to not change speed.
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can only move along Z axis
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in anydirections.
                }
            }

        }
        //If no collision , player can move.
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void HandleInteraction()
    {
        Vector3 moveDir;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }    

        float interactionDistance = 2f;

        if(Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycasthit, interactionDistance , countersLayerMask))
        {
            if (raycasthit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }

            // if raycast doesnt hit a clearcounter . 
            else
            {
                SetSelectedCounter(null);
            }
        }

        // if raycast doesnt hit anything.
        else
        {
            SetSelectedCounter(null);
        }

        //Debug.Log(selectedCounter);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
