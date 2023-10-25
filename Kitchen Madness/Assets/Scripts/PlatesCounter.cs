using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoval;

    [SerializeField] private KitchenObjectSO plateSO;

    //spawning plates
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    //Amount of plates to spawn
    private float plateSpawnedAmount;
    private float plateSpawnedAmountMax = 4f;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;

             if(KitchenGameManager.Instance.IsGamePlaying() && plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }


        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            //player is emptyhanded
            if(plateSpawnedAmount>0)
            {
                //plates are present . give one to player

                KitchenObject.SpawnKitchenObject(plateSO, player);
                plateSpawnedAmount--;

                OnPlateRemoval?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
