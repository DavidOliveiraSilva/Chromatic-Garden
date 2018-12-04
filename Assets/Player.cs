using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    private float angle;
    public float monitor;
    public bool canMove;
    public List<GameObject> field;
    public List<SpringJoint2D> grab;
    [Range(0.0f, 1.0f)]
    public float grabDistance;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (canMove) {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            angle = Mathf.Atan2(ver, hor);
            monitor = hor;
            if(Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
                
                rb.velocity = new Vector2(speed * Mathf.Cos(angle) * Mathf.Abs(hor) * Time.fixedDeltaTime, speed * Mathf.Sin(angle) * Mathf.Abs(ver) * Time.fixedDeltaTime);
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if (Input.GetButton("Grab")) {
            Grab();
        }
        if (Input.GetButton("LetGo")) {
            LetGo();
        }
	}
    public void AddOrbOnField(GameObject orb) {
        field.Add(orb);
    }
    public void RemoveOrbFromField(GameObject orb) {
        field.Remove(orb);
    }
    public void Grab() {
        LetGo();
        foreach(GameObject o in field) {
            SpringJoint2D sj = gameObject.AddComponent<SpringJoint2D>();
            grab.Add(sj);
            sj.connectedBody = o.GetComponent<Rigidbody2D>();
            sj.dampingRatio = 1f;
            sj.frequency = 1.5f;
            sj.enableCollision = true;
            sj.distance = Distance(transform.position, o.transform.position)*grabDistance;
        }
    }
    public void LetGo() {
        foreach (SpringJoint2D o in grab) {
            Destroy(o);
        }
        grab.Clear();
    }
    float Distance(Vector3 p1, Vector3 p2) {
        return Mathf.Sqrt((p1.x - p2.x)* (p1.x - p2.x) + (p1.y - p2.y)* (p1.y - p2.y));
    }
    public void TransportAllOrbs(float dx, float dy) {
        foreach (SpringJoint2D sj in grab) {
            print(sj.connectedBody.gameObject.transform.position);
            sj.connectedBody.gameObject.transform.position = AproximatePosition();
            print(sj.connectedBody.gameObject.transform.position);
            print(sj.connectedBody.gameObject.name);
        }
    }
    Vector3 AproximatePosition() {
        return new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f), transform.position.z);
    }
}
