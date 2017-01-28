using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public Rigidbody2D rigidBody;
	public bool canMove = true;
	public bool isAI = false;
	public GameObject bullet;

	void Start () {		
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		if ( canMove ){
			rigidBody.AddRelativeForce( new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) );
		}

		if (Input.GetKeyDown("space")){
			ShootBullet();
		}
	}

	void ShootBullet () {
		Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)) ,transform.position.z), transform.rotation );
	}

}
