using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container; // Overall recipe display container
    [SerializeField] private Transform recipeTemplate; // individual recipe display container

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerRecipeCompleted;   
        
    }

    private void DeliveryManagerRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManagerRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform recipeTemplateChild in container)
        {
            if(recipeTemplateChild ==  recipeTemplate)
            {
                continue;
            }
            else
            {
                Destroy(recipeTemplateChild.gameObject);
            } 
            
        }

        foreach (RecipeSO RecipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(RecipeSO);
        }
    }
}
