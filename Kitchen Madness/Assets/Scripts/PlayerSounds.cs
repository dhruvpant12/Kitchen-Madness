using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
     

    private Player player;
    private float footStepTimer;
    private float footStepTimerMax=0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer<0)
        {
            footStepTimer = footStepTimerMax;
            float footStepVolume = 1f;

            if(player.IsWalking())
            {
                SoundManager.Instance.PlayFootSteps(player.transform.position, footStepVolume);

            }
        }
    }

}
