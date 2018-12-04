using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGravity : MonoBehaviour {
    private Rigidbody2D rb;
    private float angle;
    private float distance;
    public float RepulsiveForce;
    public float radius;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Transform t in transform) {
            if(t.name == "link") {
                GameObject orb = t.GetComponent<Linking>().receiver;
                angle = Mathf.Atan2(transform.position.y - orb.transform.position.y, transform.position.x - orb.transform.position.x);
                distance = GetComponent<Orb>().Distance(transform.position, orb.transform.position);
                float df = Mathf.Abs(distance - radius);
                if(distance < radius) {
                    Vector2 force = new Vector2(RepulsiveForce * Mathf.Cos(angle + Mathf.PI), RepulsiveForce * Mathf.Sin(angle + Mathf.PI));
                    orb.GetComponent<Rigidbody2D>().AddForce(force*df);
                } else {
                    Vector2 force = new Vector2(RepulsiveForce * Mathf.Cos(angle), RepulsiveForce * Mathf.Sin(angle));
                    orb.GetComponent<Rigidbody2D>().AddForce(force*df);
                }
            }
        }
    }
}
