using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameobject; 
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameobject.GetComponent<IHasProgress>();
        if(hasProgress==null)
        {
            Debug.LogError("  aa");
        }
        hasProgress.OnProgressChanged += IhasProgressEventFire;
        barImage.fillAmount = 0;

        Hide();
    }

    private void IhasProgressEventFire(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalised;

        if(e.progressNormalised == 0 || e.progressNormalised == 1) // if the progress bar is completly empty or full , then hide it.
        {
            Hide();
        }
        else
        {
            Show(); 
        }
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

