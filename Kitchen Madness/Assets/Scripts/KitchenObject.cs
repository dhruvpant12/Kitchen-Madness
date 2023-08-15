using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;


    //Setting new counter
    public void SetClearCounter(ClearCounter clearCounter)
    {
        // if counter is not emplty , clear the item on it.
        if(this.clearCounter == null)
        {
            clearCounter.ClearKitchenObject();
        }

        // assign new counter
        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("counter has soemthing on it already");
        }
        clearCounter.SetKitchenObject(this); // the kitchen object never be assign to a counter that is holding something.

        // change position and graphics of object.
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
