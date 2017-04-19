using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscTurner : MonoBehaviour
{

    public Rigidbody2D DiscRef;
    public float AngleAccelPerSecond = 50;
    public float MaxAngularVelocity = 50;

	// Use this for initialization
	void Start ()
    {
        if (!DiscRef)
        {
            DiscRef = Disc.Instance.GetComponent<Rigidbody2D>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnCw();
        }

        //Rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnCcw();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PressPause();
        }
    }

    //Clockwise
    void TurnCw()
    {
        //Keep below max speed
        if (DiscRef.angularVelocity < -MaxAngularVelocity)
        {
            return;
        }
        DiscRef.angularVelocity -= AngleAccelPerSecond * Time.deltaTime;
    }

    //Counterclockwise
    void TurnCcw()
    {
        //Keep below max speed
        if (DiscRef.angularVelocity > MaxAngularVelocity)
        {
            return;
        }
        DiscRef.angularVelocity += AngleAccelPerSecond * Time.deltaTime;
    }

    void PressPause()
    {
        Time.timeScale = ((Time.timeScale + 1) % 2);
    }
}
