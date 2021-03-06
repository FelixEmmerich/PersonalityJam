﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    public GameObject Target;
    public float Speed;
    public int Connections = 0;
    public int RequiredConnections;
    public bool moving = false;

    private void Start()
    {
        Target = Disc.Instance.gameObject;
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Don't fire events on both FOs
        if (!moving)
        {
            return;
        }

        //Collided with disc
        if (other.gameObject==Target)
        {
            Debug.Log("Collided with disc");
            transform.parent = other.transform;
            Destroy(GetComponent<Rigidbody2D>());
            moving = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            Debug.Log("Do your thing with the disc");
            GameManager.instance.AddBubble(gameObject);
            return;
        }

        Bubble bubbleComp = other.GetComponent<Bubble>();

        //No collisions on two moving objects
        if (bubbleComp&&!bubbleComp.moving)
        {
            transform.parent = other.transform.parent;
            Debug.Log("Do your thing with the bubble");
            GameManager.instance.AddBubble(gameObject);
            Destroy(GetComponent<Rigidbody2D>());
            moving = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;

            List<Collider2D> allNeighbours = new List<Collider2D>();
            allNeighbours.Add(gameObject.GetComponent<Collider2D>());

            List<Collider2D> relatedNeighbours = GameManager.instance.CheckRelatedNeighbours(allNeighbours[0],allNeighbours);

            GameManager.instance.GenerateScore(relatedNeighbours);
        }
    }
}