using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    public Text text;
	public void Start()
    {
        GetComponent<Text>().text = GameManager.instance.Score.ToString();
    }
}
