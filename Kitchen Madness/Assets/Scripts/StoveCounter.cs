using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter , IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<stoveCounterParticlesEventArgs> OnstoveCounterParticles;

    public class stoveCounterParticlesEventArgs : EventArgs
    {
       public bool showParticles;
    }

    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt,
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private float fryingTimer;
    private float burningTimer;
    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    showVisual(false);
                    break;
                case State.Frying:

                    //Event fired up for progress bar UI
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalised = fryingTimer / fryingRecipeSO.FryingTimerMax
                    });


                    // starting animation of stove counter visuals.
                    showVisual(true);

                    fryingTimer += Time.deltaTime;

                    if (fryingTimer >= fryingRecipeSO.FryingTimerMax)
                    {
                        //Fried. destroy uncooked patty and switch it with cooked patty                        
                        GetKitchenObject().DestroySelf();

                        //Cooked patty spawn and stove set as its parent.
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        //setting up burning timer . 
                        burningTimer = 0;

                        state = State.Fried;

                        //getting reference to the cooked patty instance.
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


                    }

                    break;

                case State.Fried:

                    //Evenet fired for progress bar UI
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalised = burningTimer / burningRecipeSO.burningTimerMax
                    });


                    showVisual(true);

                    burningTimer += Time.deltaTime;

                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        //Fried. destroy uncooked patty and switch it with cooked patty                        
                        GetKitchenObject().DestroySelf();

                        //Cooked patty spawn and stove set as its parent.
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burnt;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalised = 0
                        });

                    }

                    break;

                case State.Burnt:
                    showVisual(false);
                    break;
            }
        }
        
    }

    private void showVisual(bool showVisuals)
    {
        OnstoveCounterParticles?.Invoke(this, new stoveCounterParticlesEventArgs
        {
            showParticles = showVisuals
        });

    }
    public override void Interact(Player player)
    {
        //if no kitchen object is on the counter . 
        if (!HasKitchenObject())
        {

            //check if the player is holding any kitchen object 
            if (player.HasKitchenObject())
            {

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //PLyaer is carrying an uncooked patty. 
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalised =  fryingTimer / fryingRecipeSO.FryingTimerMax
                    });
                }
            }
            else
            {
                //player carrying nothing. So do nothing
            }
        }
        else
        {
            // there is a kitchen object on the counter 
            if (player.HasKitchenObject())
            {
                //player has something . check for plate.
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        showVisual(false);
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalised = 0
                        });
                    }
                }
            }
            else
            {
                //player has nothing , so transfer kitchen object on the counter to the player.
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                showVisual(false);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalised = 0
                });
            }
        }
    }

    //Check if the kitchen object can be cut into other items . For eg , bread cannot be cut but tomato can be.
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    //Returns Kitchen SO after cutting the Kitchen Object.
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == InputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == InputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}
