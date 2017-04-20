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

    public ContactFilter2D bubbleFilter;

    public int Score;

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


    public List <Collider2D> CheckRelatedNeighbours(Collider2D currentBubble, List<Collider2D> relatedNeighbours)
    {
        Debug.Log("Checking for related neighbours...");
        Collider2D[] neighbours = GameManager.instance.CheckNeighbours(currentBubble);
        Debug.Log("Current blurble: " + currentBubble);

        int counter = 0;

        foreach (Collider2D neighbour in neighbours)
        {
            counter++;
            Debug.Log("Daten: " + counter + " Tag: " + (neighbour.tag ==
                currentBubble.tag) + "contained: " + relatedNeighbours.Contains(neighbour));

            if (neighbour.tag == 
                currentBubble.tag && 
                !relatedNeighbours.Contains(neighbour)) {
                relatedNeighbours.Add(neighbour);
                Debug.Log("Related Neighbour"+ neighbour +" added");
                relatedNeighbours=CheckRelatedNeighbours(neighbour, relatedNeighbours);
            }
        }
        //Debug.Log("Related Neighbours are: " + relatedNeighbours[0] + relatedNeighbours[1] + relatedNeighbours[2] + relatedNeighbours[3] + relatedNeighbours[4]);

        return relatedNeighbours;
    }

    public void GenerateScore(List<Collider2D> colliders)
    {
        int numBubbles = colliders.Count;
        switch (numBubbles)
        {
            case 0: break;
            case 1: break;
            case 2: break;
            case 3: Score += numBubbles; DestroyBubbles(colliders); break;
            case 4: Score += numBubbles*2; DestroyBubbles(colliders); break;
            case 5: Score += numBubbles*3; DestroyBubbles(colliders); break;
            default: DestroyBubbles(colliders); break;
        }
    }

    public void DestroyBubbles(List<Collider2D> colliders)
    {
        foreach (Collider2D bubble in colliders)
        {
            Destroy(bubble.gameObject);
        }
    }

    public Collider2D[]CheckNeighbours(Collider2D currentBubble)
    {
        Collider2D[] neighbours = new Collider2D[5];
        //ContactFilter2D bubbleFilter = new ContactFilter2D();
        //bubbleFilter.SetLayerMask(1 << LayerMask.NameToLayer("Bubble"));
        //currentBubble.OverlapCollider(bubbleFilter,neighbours);
        Debug.Log("Radius:" + ((CircleCollider2D)currentBubble).radius * 1.1f);
        neighbours = Physics2D.OverlapCircleAll(currentBubble.gameObject.transform.position, ((CircleCollider2D)currentBubble).radius*(1.0f/currentBubble.gameObject.transform.localScale.x), bubbleFilter.layerMask);
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