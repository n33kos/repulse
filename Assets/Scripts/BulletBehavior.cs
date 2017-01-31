using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	//Speed dictates how fast the projectile will travel
	public float speed = 15;
	//Power dictates the bullets impact value on collision
    public float power = 2;

	void Start () {
		//Destroy self after 5 seconds, this is a cheap and dirty way to give bullets an "age"
		Destroy(gameObject, 5);
	}
	
	void FixedUpdate () {
		//Every FixedUpdate frame, replace position with upward vector * speed / 100 to make the input value smaller
		//Multiplying a Vector3 applies the calculation to each value, in our case its only value is a positive 1 in the Y axis
		transform.position += transform.up*(speed/100);
	}

	void OnTriggerEnter2D(Collider2D col) {
		//If we ever collide with a trigger, destroy ourselves
		//This is where we wouldspawn a particle effect for impact.
		Destroy(gameObject);
	}

}