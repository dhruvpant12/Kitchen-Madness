using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;



    private KitchenObject kitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject!=null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact()
    {

        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);


            // Kitchen object will be assigned a new counter . 
            // the new counter will be assigned this kitchen object
            // its position will be updated using its parent's transform
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
           
        }
        else
        {
            // Which counter the object is on.
            Debug.Log(kitchenObject.GetClearCounter());
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
