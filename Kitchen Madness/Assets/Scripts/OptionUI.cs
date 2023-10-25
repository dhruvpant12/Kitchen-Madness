using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Button moveUPButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactALTButton;
    [SerializeField] private Button pauseButton;


    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactALTText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;



    private bool togglePanel = false;


    private void Awake()
    {
        Instance = this;
        TogglePanel();

        soundButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        backButton.onClick.AddListener(() => {
            TogglePanel();
        });


        //Rebinding Buttons
        moveUPButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Move_UP);});
        moveDownButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Move_Down);});
        moveLeftButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Move_Left);});
        moveRightButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Move_Right);});
        interactButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Interact);});
        interactALTButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.InteractAlt);});
        pauseButton.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Pause);});
    }

    private void Start()
    {
        UpdateVisuals();
        HideRebindPanel();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        // If user presses Esc , it will close both options and pause panel. First check if Esc is pressed , then check if game is paused.
        if(!KitchenGameManager.Instance.IsGamePaused())
        {
            TogglePanel();
        }
    }

    private void UpdateVisuals()
    {
        soundEffectText.text ="Sound Effect : " + SoundManager.Instance.GetVolume().ToString();
        musicEffectText.text = "Music  : " + MusicManager.Instance.GetVolume().ToString();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_UP);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        interactALTText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
    }

    public void TogglePanel()
    {
        gameObject.SetActive(togglePanel);
        togglePanel = !togglePanel;
    }

    private void ShowRebindPanel()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HideRebindPanel()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Bindings binding)
    {
        ShowRebindPanel();


        //using delegate to hide the rebind panel and update the new rebinded keys
        GameInput.Instance.RebindBinding(binding, () =>
         {
             HideRebindPanel();
             UpdateVisuals();
         });
    }
}
