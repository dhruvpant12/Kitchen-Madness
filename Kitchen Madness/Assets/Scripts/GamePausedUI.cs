using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;


    private bool toggle;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            TogglePausePanel();
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(()=> { Loader.Load(Loader.Scene.MainMenuScene); });

        optionsButton.onClick.AddListener(() => { OptionUI.Instance.TogglePanel(); });
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        gameObject.SetActive(false);
        toggle = true;
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        TogglePausePanel();
    }

    private void TogglePausePanel()
    {
        gameObject.SetActive(toggle);
        toggle = !toggle;
    }

    private void GoToMainMenu()
    {
         
    }
}
