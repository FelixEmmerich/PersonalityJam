using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour {

    public GameObject Target;
    public float Speed;

    private void Awake()
    {
        Target = Disc.Instance.gameObject;
    }

    void Update()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
    }

}
