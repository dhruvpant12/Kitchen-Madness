using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // This object's Scriptable object.

    private ClearCounter clearCounter; // What counter this KO is sitting on.


    //Setting new counter
    public void SetClearCounter(ClearCounter clearCounter)
    {
        // Clearing old kitchen counter
        // if counter is not emplty , clear the item on it.
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        // assign new counter
        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("counter has soemthing on it already");
        }
        clearCounter.SetKitchenObject(this); // the kitchen object never be assign to a counter that is holding something.

        // change parent, position and graphics of object and update the graphics.
        transform.parent = clearCounter.GetKichtenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
