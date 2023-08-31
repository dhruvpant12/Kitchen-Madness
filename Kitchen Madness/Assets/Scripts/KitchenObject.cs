using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // This object's Scriptable object.

    private IKitchenObjectParent kitchenObjectParent; // What counter this KO is sitting on.


    //Setting new counter
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent )
    {
        // Clearing old kitchen counter
        // if counter is not emplty , clear the item on it.
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        // assign new parent
        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("kitchenobjectparent has soemthing on it already");
        }
        kitchenObjectParent.SetKitchenObject(this); // assigning kitchen object to its new parent

        // change parent, position and graphics of object and update the graphics.
        transform.parent = kitchenObjectParent.GetKichtenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
