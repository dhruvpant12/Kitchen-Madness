using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private bool isSoundPlaying;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
         
    }

    private void Start()
    {
        stoveCounter.OnstoveCounterParticles += StoveCounter_OnstoveCounterParticles;
        isSoundPlaying = false;
    }

    private void StoveCounter_OnstoveCounterParticles(object sender, StoveCounter.stoveCounterParticlesEventArgs e)
    {

        bool playAudio = e.showParticles;

        if (playAudio)
        {
            if (!isSoundPlaying)
            {
                audioSource.Play();
                isSoundPlaying = true;

            }
        }
        else
        {
            audioSource.Pause();
            isSoundPlaying = false;
        }
            

           
    }
}
