using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
        ShowCountDownVisual(false);
    }

    private void Update()
    {
        countDownText.text = (Mathf.Ceil(KitchenGameManager.Instance.ShowCountDownToStartTimer())).ToString();
    }

    private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountDownStateActive())
            ShowCountDownVisual(true);
        else
            ShowCountDownVisual(false);

    }

    private void ShowCountDownVisual(bool showVisual)
    {
        gameObject.SetActive(showVisual);
    }
}


