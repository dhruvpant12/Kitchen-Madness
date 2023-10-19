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
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;

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
    }

    private void Start()
    {
        UpdateVisuals();

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
    }

    public void TogglePanel()
    {
        gameObject.SetActive(togglePanel);
        togglePanel = !togglePanel;
    }
}
