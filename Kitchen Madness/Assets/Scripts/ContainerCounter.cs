using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter 
{
    //Event to play animation when player grabs an object from the container.
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
 
    public override void Interact(Player player)
    {
        //player interacts and the container spawns an object and gives it to the player 

        //player is not carrying anything. So give an item to player.
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
           

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty); // lid animation.
        }
    }

  
}
