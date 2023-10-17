using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance { get; private set; }


    //Various States of game 
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingToStartTimer = 10f;
     

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer<0)
                {
                    state = State.CountDownToStart;
                }
                break;

            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                }
                break;

            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0)
                {
                    state = State.GameOver;
                }
                break;

            case State.GameOver:break;
        }
        
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
