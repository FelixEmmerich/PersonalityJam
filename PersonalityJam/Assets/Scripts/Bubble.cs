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
            return;
        }

        Bubble foComp = other.GetComponent<Bubble>();

        //No collisions on two moving objects
        if (foComp&&!foComp.moving)
        {
            transform.parent = other.transform;
            Destroy(GetComponent<Rigidbody2D>());
            moving = false;

            //Todo: Tags
            if (other.tag == tag)
            {
                Debug.Log("Collided with other FO");
                foComp.CreateConnection();
            }
        }
    }

    public void CreateConnection()
    {
        Connections++;
        //todo: Update connected FOs
    }
}