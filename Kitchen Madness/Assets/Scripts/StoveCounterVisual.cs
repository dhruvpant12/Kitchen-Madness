using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesOnGameObject;


    [SerializeField] private StoveCounter stoveCounter;


    private void Start()
    {
        stoveCounter.OnstoveCounterParticles += StoveCounter_OnstoveCounterParticles;
    }

    private void StoveCounter_OnstoveCounterParticles(object sender, StoveCounter.stoveCounterParticlesEventArgs e)
    {
        stoveOnGameObject.SetActive(e.showParticles);
        particlesOnGameObject.SetActive(e.showParticles);
    }
}