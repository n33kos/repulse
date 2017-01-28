using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public float speed = 25;

	void Start () {
		Destroy(gameObject, 20);
	}
	
	void FixedUpdate () {
		transform.position += transform.up*(speed/100);
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Destroy(gameObject);
	}

}