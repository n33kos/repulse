using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public float speed = 15;
    public float power = 2;

	void Start () {
		Destroy(gameObject, 5);
	}
	
	void FixedUpdate () {
		transform.position += transform.up*(speed/100);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Destroy(gameObject);
	}

}