using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    public static event EventHandler OnDropOffSomething;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject; // What KO is on the counter.

    public virtual void Interact(Player player)
    {

    }

    public virtual void InteractAlternate(Player player)
    {

    }

    public Transform GetKichtenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
            OnDropOffSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }


    // return if an object is on ut.
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
