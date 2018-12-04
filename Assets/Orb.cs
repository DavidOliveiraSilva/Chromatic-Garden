using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
    public Cor cor;
    private Connections connections;
    public GameObject link;
    private SpriteRenderer sr;
    private List<GameObject> orbs;
    //public ParticleSystem shine;
    public int receiving;
    // Use this for initialization
	void Start () {
        connections = GameObject.Find("GameManager").GetComponent<Connections>();
        sr = GetComponent<SpriteRenderer>();
        //shine = transform.Find("Shine").GetComponent<ParticleSystem>();
        //var psmain = shine.main;
        //psmain.startColor = sr.color;
    }
	
	// Update is called once per frame
	void Update () {
        
		foreach(Transform t in transform) {
            if (t.name == "link") {
                if (Distance(transform.position, t.gameObject.GetComponent<Linking>().receiver.transform.position) < 3) {
                    if (!t.GetComponent<ParticleSystem>().isEmitting) {
                        t.GetComponent<ParticleSystem>().Play();
                        //t.GetComponent<Linking>().receiver.GetComponent<Orb>().shine.Emit(10);
                    }
                } else {
                    t.GetComponent<ParticleSystem>().Stop();
                    
                }
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().AddOrbOnField(gameObject);
        }
        if(collision.gameObject.tag == "Orb") {
            if (connections.DoesConect(cor, collision.gameObject.GetComponent<Orb>().cor) && !HasLink(collision.gameObject)) {
                GameObject l = Instantiate(link);
                l.transform.position = transform.position;
                l.transform.SetParent(transform);
                l.name = "link";
                l.GetComponent<Linking>().emitter = gameObject;
                l.GetComponent<Linking>().receiver = collision.gameObject;
                ParticleSystem ps = l.GetComponent<ParticleSystem>();
                ParticleSystem.ColorOverLifetimeModule co = ps.colorOverLifetime;
                Color other = collision.gameObject.GetComponent<SpriteRenderer>().color;
                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(sr.color, 0.0f), new GradientColorKey(other, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
                co.color = grad;
                float dy = transform.position.y - collision.transform.position.y;
                float dx = transform.position.x - collision.transform.position.x;
                float angle = Mathf.Atan2(dy, dx)*Mathf.Rad2Deg;
                l.transform.eulerAngles = new Vector3(0, 0, angle);
                //collision.gameObject.GetComponent<Orb>().shine.Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().RemoveOrbFromField(gameObject);
        }
    }
    private bool HasLink(GameObject orb) {
        foreach(Transform t in transform) {
            if(t.name == "link" && t.gameObject.GetComponent<Linking>().receiver == orb) {
                return true;
            }
        }
        return false;
    }
    public float Distance(Vector3 p1, Vector3 p2) {
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
    }
}
