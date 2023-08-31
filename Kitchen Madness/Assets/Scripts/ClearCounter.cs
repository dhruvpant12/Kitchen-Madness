using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour ,IKitchenObjectParent 
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;    



    private KitchenObject kitchenObject; // What KO is on the counter.

     

    public void Interact(Player player)
    {
        //only spawn new object is counter top is empty.
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);


            // Kitchen object will be assigned a new counter . 
            // the new counter will be assigned this kitchen object
            // its position will be updated using its parent's transform
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
           
        }

        //if counter is not empty , then give the object to the player
        else
        {
            kitchenObject.SetKitchenObjectParent(player);

            // Which counter the object is on.
           // Debug.Log(kitchenObject.GetClearCounter());
        }

       
    }

    public Transform GetKichtenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
