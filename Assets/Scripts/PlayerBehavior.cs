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
		if ( isAI ){
			rigidBody.AddRelativeForce( new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 3.75f)) );
			if( Random.Range(0f, 100f) < 5 ){
				ShootBullet();
			}
		}else{
			if ( canMove ){
				rigidBody.AddRelativeForce( new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) );
			}
			if (Input.GetKeyDown("space")){
				ShootBullet();
			}
		}
		ConstrainToWindow();
	}

	void ShootBullet () {
		Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)) ,transform.position.z), transform.rotation );
	}

	void ConstrainToWindow () {
		Vector3 tran = new Vector3();
		if(transform.position.x < -3.75f){
			rigidBody.velocity = new Vector2( Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}else if(transform.position.x > 3.75f){
			rigidBody.velocity = new Vector2( -Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}
	}

}
