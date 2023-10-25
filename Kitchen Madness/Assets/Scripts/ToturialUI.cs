using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ToturialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDown;
    [SerializeField] private TextMeshProUGUI keyMoveRight;
    [SerializeField] private TextMeshProUGUI keyMoveLeft;
    [SerializeField] private TextMeshProUGUI keyMoveInteract;
    [SerializeField] private TextMeshProUGUI keyMoveInteractAlt;
    [SerializeField] private TextMeshProUGUI keyPause;



    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        UpdateVisuals();

        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;

    }

    private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCountDownStateActive())
            Hide();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        

        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_UP);
        keyMoveDown.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Down);
        keyMoveLeft.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Left);
        keyMoveRight.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Right);
        keyMoveInteract.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        keyMoveInteractAlt.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlt);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
 