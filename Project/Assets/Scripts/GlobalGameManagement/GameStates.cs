using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
    {
        Play,
        Pause,
        Restart,
        FullReset
    }
public class GameStates : MonoBehaviour
{
    public static GameStates instance {get; private set;}
    public GameState CurrentState{get; private set;}
    

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else Destroy(gameObject);
    }

    void Start()
    {
        StateMachine(GameState.Play);
    }



    public void StateMachine(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Play:
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
                Time.timeScale =0f;
                break;
            case GameState.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameState.FullReset:
                SceneManager.LoadScene("Level1-1");
                break;
        }
    }


}
