using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    private float angle;
    public float monitor;
    public bool canMove;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (canMove) {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            monitor = hor;
            if(Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
                angle = Mathf.Atan2(ver, hor);
                rb.velocity = new Vector2(speed * Mathf.Cos(angle) * Mathf.Abs(hor) * Time.fixedDeltaTime, speed * Mathf.Sin(angle) * Mathf.Abs(ver) * Time.fixedDeltaTime);
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }
	}
}
