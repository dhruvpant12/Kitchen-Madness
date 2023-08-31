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
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab );         
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

  
}
