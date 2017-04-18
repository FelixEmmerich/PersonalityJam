using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscTurner : MonoBehaviour
{

    public Rigidbody2D Disc;
    public float AngleAccelPerSecond;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Disc.angularVelocity -= AngleAccelPerSecond * Time.deltaTime;
        }

        //Rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Disc.angularVelocity += AngleAccelPerSecond * Time.deltaTime;
        }
    }
}
