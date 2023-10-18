using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{

    public event EventHandler OnStateChange;
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
    private float gamePlayingToStartTimer ;
    private float gamePlayingToStartTimerMax = 10f;
     

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
                OnStateChange?.Invoke(this, EventArgs.Empty);
                break;

            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                }
                OnStateChange?.Invoke(this, EventArgs.Empty);

                break;

            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0)
                {
                    state = State.GameOver;
                }
                OnStateChange?.Invoke(this, EventArgs.Empty);

                break;

            case State.GameOver:break;
        }
        
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownStateActive()
    {
        return state == State.CountDownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float ShowCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalised()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }
}
