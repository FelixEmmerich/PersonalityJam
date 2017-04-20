using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  //static instance of GameManager to be visible by/accessible from any other script; initialize as null so there is none in the first place
    public enum GameState { playing, gameOver };
    public GameState CurrentState = GameState.playing;
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


    public List <Collider2D> CheckRelatedNeighbours(Collider2D currentBubble)
    {
        Debug.Log("Checking for related neighbours...");
        List<Collider2D> relatedNeighbours = new List<Collider2D>();
        Collider2D[] neighbours = GameManager.instance.CheckNeighbours(currentBubble);

        foreach (Collider2D neighbour in neighbours)
        {
            if (neighbour.tag == currentBubble.tag && !relatedNeighbours.Contains(currentBubble)) {
                relatedNeighbours.Add(neighbour);
                Debug.Log("Related Neighbour"+ neighbour +" added");
            }
        }
        Debug.Log("Related Neighbours are: " + relatedNeighbours[0] + relatedNeighbours[1] + relatedNeighbours[2] + relatedNeighbours[3] + relatedNeighbours[4]);

        return relatedNeighbours;
    }

    public Collider2D[]CheckNeighbours(Collider2D currentBubble)
    {
        Collider2D[] neighbours = new Collider2D[5];
        ContactFilter2D bubbleFilter = new ContactFilter2D();
        bubbleFilter.SetLayerMask(LayerMask.NameToLayer("Bubble"));
        Debug.Log("Bubble Layer Number:"+ LayerMask.NameToLayer("Bubble"));
        Debug.Log("LayerMask number for bubbles is "+bubbleFilter.layerMask);
        currentBubble.OverlapCollider(bubbleFilter,neighbours);
        return neighbours;
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