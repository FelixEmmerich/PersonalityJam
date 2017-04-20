using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  //static instance of GameManager to be visible by/accessible from any other script; initialize as null so there is none in the first place
    public enum GameState { playing, gameOver };
    public GameState CurrentState = GameState.playing;
    public int bubblesMax;
    [SerializeField]
    private int _bubbleCount = 0;

    public ContactFilter2D bubbleFilter;

    public int Score;

    public string Endlevel;

    bool over = false;

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
        Collider2D[] neighbours = GameManager.instance.CheckNeighbours(currentBubble);

        int counter = 0;

        foreach (Collider2D neighbour in neighbours)
        {
            counter++;

            if (neighbour.tag == 
                currentBubble.tag && 
                !relatedNeighbours.Contains(neighbour)) {
                relatedNeighbours.Add(neighbour);
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
        GraphicsManager grM = GraphicsManager.Instance;
        bubbleCount = -colliders.Count;
        foreach (Collider2D bubble in colliders)
        {

            GameObject go = Instantiate(grM.DestructionEffects[Random.Range(0, grM.DestructionEffects.Length)]);
            go.transform.position = bubble.transform.position;
            Destroy(bubble.gameObject);
        }
    }

    public Collider2D[]CheckNeighbours(Collider2D currentBubble)
    {
        Collider2D[] neighbours = new Collider2D[5];
        //ContactFilter2D bubbleFilter = new ContactFilter2D();
        //bubbleFilter.SetLayerMask(1 << LayerMask.NameToLayer("Bubble"));
        //currentBubble.OverlapCollider(bubbleFilter,neighbours);
        neighbours = Physics2D.OverlapCircleAll(currentBubble.gameObject.transform.position, ((CircleCollider2D)currentBubble).radius *(currentBubble.gameObject.transform.localScale.x), bubbleFilter.layerMask);
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
                if (over)
                {
                    break;
                }
                over = true;
                Debug.Log("Gameover!");
                SceneManager.LoadScene("Endscreen");
                Destroy(Disc.Instance.gameObject);
                CurrentState = GameState.playing;
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
            bubbleCount = 0;
            CurrentState = GameState.gameOver;
            Debug.Log("GameState changed to"+CurrentState);
            CheckGameState();
        }
    }
}