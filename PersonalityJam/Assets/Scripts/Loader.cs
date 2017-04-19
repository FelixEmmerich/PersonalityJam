using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;  //the GameManager prefab to instantiate
    void Awake()
    {
        //check if a GameManager has already been assigned to static variable GameManager.instance
        if (GameManager.instance == null)
            //instantiate the gameManager prefab
            Instantiate(gameManager);
    }
}