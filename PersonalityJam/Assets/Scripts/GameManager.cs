using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  //static instance of GameManager to be visible by/accessible from any other script; initialize as null so there is none in the first place
    enum GameState { playing, gameOver };
    private GameState CurrentState = GameState.playing;
    public int bubblesMax;
    [SerializeField]
    private int _bubbleCount = 0;
    public GameObject[] bubbleCombo;

    //===============================================================
    //Getter / Setter
    //===============================================================
    
    public int bubbleCount
    {
        get { return _bubbleCount; }
        set { _bubbleCount += value; Debug.Log("BubbleCount: "+_bubbleCount); }
    }

    void Awake()
    {
        //check if instance already exists
        if (instance == null)
            //if not, set instance to this instance
            instance = this;
        //if instance already exists and it's not this instance:
        else if (instance != this)
            //then destroy this instance so there can only ever be one instance of our GameManager
            Destroy(gameObject);
        //sets this to not be destroyed when reloading the scene
        DontDestroyOnLoad(gameObject);
        bubblesMax = 150;
    }



    private void CheckGameState()
    {
        
        switch (CurrentState)
        {
            case GameState.playing:
                Debug.Log("The game is playing!");
                break;
            case GameState.gameOver:
                Debug.Log("Gameover!");
                break;
            default:
                Debug.Log("Nothing!");
                break;
        }
    }

    private void FixedUpdate()
    {

    }

    void Update()
    {
        if(bubbleCount >= bubblesMax)
        {
            CurrentState = GameState.gameOver;
            Debug.Log("GameState changed to"+CurrentState);
        }
        CheckGameState();
    }
}