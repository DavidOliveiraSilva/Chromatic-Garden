using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linking : MonoBehaviour {
    public GameObject emitter;
    public GameObject receiver;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float dy = emitter.transform.position.y - receiver.transform.position.y;
        float dx = emitter.transform.position.x - receiver.transform.position.x;
        float angle = Mathf.Atan2(-dy, -dx) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
	}
}
