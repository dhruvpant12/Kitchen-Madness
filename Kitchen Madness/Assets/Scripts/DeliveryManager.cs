using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnCorrectRecipeDeliveryText;



    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO; // this contains the list of all the recipeSO. It will randonly generate recipe request for the player.
     private List<RecipeSO> waitingRecipeSOList; // This contains the list of all recipes requested by customer . 


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0)
        {
            //create new recipe request when timer runs out. Waitinglist can hold only a limited amount of recipes.
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)]; // get random recipe for the recipeListSO list.
                 waitingRecipeSOList.Add(recipeSO); // add to waitingRecipeList.
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        //player delivered plate with contents. Check plate content against recipes in the waiting recipe lists.

        for(int i=0; i < waitingRecipeSOList.Count; i++)
        {
            //cycling through the recipes in the waiting list.
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i]; 

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //number of items is same on both recipes. Possible match! Now check for individual ingredients.

                bool plateContentMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through the ingredients of the recipe in the waiting list.
                    bool ingredientsFound = false;

                    foreach(KitchenObjectSO plateKitchenObhjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through the ingredients of the plate. 

                        if(plateKitchenObhjectSO== recipeKitchenObjectSO)
                        {
                            //One ingredient has matched.
                            ingredientsFound = true;
                            break;
                        }
                    }

                    if(!ingredientsFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }

                if(plateContentMatchesRecipe)
                {
                    //player delivered correct recipe. Exit of out loop and remove recipe for the waiting list.
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    OnCorrectRecipeDeliveryText?.Invoke(this, EventArgs.Empty);


                    return;
                }
                else
                {
                    //check next recipe in the waiting list.
                }
            }
        }

        // The player did not deliver the correct recipe. 
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);


    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList; 
    }
}
