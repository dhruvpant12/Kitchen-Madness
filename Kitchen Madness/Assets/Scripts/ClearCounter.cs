using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter 
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        //if no kitchen object is on the counter . 
        if (!HasKitchenObject())        {
            
            //check if the player is holding any kitchen object 
            if (player.HasKitchenObject())
            {
                //player has somthing to transfer it to the counter.
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player carrying nothing. So do nothing
            }
        }
        else
        {
            // there is a kitchen object on the counter 
            if(player.HasKitchenObject())
            {
                //player has something

                //check if the object player is holding is a plate.
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
                }
                else
                {
                    //player is not holding plate but something else . referencing the counter.
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        //counter has plate on it.
                        if (plateKitchenObject.TryAddIngredients(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            //destroy plate
                            player.GetKitchenObject().DestroySelf();

                        }
                    }
                }
            }
            else
            {
                //player has nothing , so transfer kitchen object on the counter to the player.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    
       
    }

 


}
