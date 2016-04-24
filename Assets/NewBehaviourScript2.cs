using UnityEngine;
using System.Collections;

public class NewBehaviourScript2 : MonoBehaviour {

    public int speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime, speed, 0);
    }
}
