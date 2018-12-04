using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPortal : MonoBehaviour {
    public GameObject pair;
    public bool active;
    public float activeDelay;
    private float activeCounter;
	// Use this for initialization
	void Start () {
        activeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!active) {
            activeCounter += Time.deltaTime;
            if(activeCounter >= activeDelay) {
                active = true;
                activeCounter = 0;
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (active && pair != null) {
                float dx = collision.transform.position.x - pair.transform.position.x;
                float dy = collision.transform.position.y - pair.transform.position.y;
                collision.transform.position = pair.transform.position;
                collision.gameObject.GetComponent<Player>().TransportAllOrbs(dx, dy);
                active = false;
                pair.GetComponent<SmallPortal>().active = false;
            }
        }
    }
}
