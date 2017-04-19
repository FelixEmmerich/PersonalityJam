using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscTurner : MonoBehaviour
{
    [System.Serializable]
    public class DiscElement
    {
        public Rigidbody2D RigidBody2D;
        public bool RightTurnsClockWise = true;
    }

    public DiscElement[] DiscElements;
    public Rigidbody2D DiscRef;
    public float AngleAccelPerSecond = 50;
    public float MaxAngularVelocity = 50;

	// Use this for initialization
	void Start ()
    {
        if (!DiscRef)
        {
            DiscRef = Disc.Instance.GetComponent<Rigidbody2D>();
            if (DiscElements.Length == 0)
            {
                DiscElements = new DiscElement[1];
                DiscElements[0] = new DiscElement { RigidBody2D = DiscRef };
            }
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Currently all elements use the same speed, so first check if the first one has reached max speed and only update the rest if not
            if (Turn(DiscElements[0].RigidBody2D, DiscElements[0].RightTurnsClockWise))
            {
                if (DiscElements.Length > 1)
                {
                    for (int i = 1; i < DiscElements.Length; i++)
                    {
                        Turn(DiscElements[i].RigidBody2D, DiscElements[i].RightTurnsClockWise);
                    }
                }
            }
        }

        //Rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Currently all elements use the same speed, so first check if the first one has reached max speed and only update the rest if not
            if (Turn(DiscElements[0].RigidBody2D, !DiscElements[0].RightTurnsClockWise))
            {
                if (DiscElements.Length > 1)
                {
                    for (int i = 1; i < DiscElements.Length; i++)
                    {
                        Turn(DiscElements[i].RigidBody2D, !DiscElements[i].RightTurnsClockWise);
                    } 
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PressPause();
        }
    }

    bool Turn(Rigidbody2D rb, bool rightTurnsClockwise)
    {
        //Keep below max speed
        if ((rightTurnsClockwise && rb.angularVelocity < -MaxAngularVelocity)||(!rightTurnsClockwise && rb.angularVelocity > MaxAngularVelocity))
        {
            return false;
        }
        rb.angularVelocity += (rightTurnsClockwise?-1:1) * AngleAccelPerSecond * Time.deltaTime;
        return true;
    }

    /*
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
    */

    void PressPause()
    {
        Time.timeScale = ((Time.timeScale + 1) % 2);
    }
}
