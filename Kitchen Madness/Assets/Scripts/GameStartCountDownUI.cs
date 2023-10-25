using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private const string COUNTDOWNTRIGGER = "NumPopUp";
    private Animator animator;
    private int previousCounterNumber;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
        ShowCountDownVisual(false);
    }

    private void Update()
    {
        int counterDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.ShowCountDownToStartTimer());
        countDownText.text = counterDownNumber.ToString();

        if(previousCounterNumber!=counterDownNumber)
        {
            previousCounterNumber = counterDownNumber;
            animator.SetTrigger(COUNTDOWNTRIGGER);
            SoundManager.Instance.PlayCountDownSound();
        }
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


