using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    int correctRecipesDelivered = 0;


    private void Start()
    {
        ShowRecipesDelivered(false);
        DeliveryManager.Instance.OnCorrectRecipeDeliveryText += DeliveryManger_OnCorrectRecipeDeliveryText;
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
    }

    private void Awake()
    {
        
    }

    private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
            ShowRecipesDelivered(true);
        else
            ShowRecipesDelivered(false);
    }

    private void DeliveryManger_OnCorrectRecipeDeliveryText(object sender, System.EventArgs e)
    {
        correctRecipesDelivered++;
        UpdateRecipeDeliveredText();
    }

    private void UpdateRecipeDeliveredText()
    {
        recipesDeliveredText.text = correctRecipesDelivered.ToString();
    }

    private void ShowRecipesDelivered(bool showVisual)
    {
        
        gameObject.SetActive(showVisual);
    }
}
