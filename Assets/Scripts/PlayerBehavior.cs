using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public Rigidbody2D rigidBody;
	public bool canMove = true;
	public bool isAI = false;
	public GameObject bullet;
	public GameObject bomb;
	public int primaryFireCooldown = 5;
	public int secondaryFireCooldown = 25;
	public int primaryFireCounter = 5;
	public int secondaryFireCounter = 25;

	void Start () {		
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		if ( isAI ){
			rigidBody.AddRelativeForce( new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 3.75f)) );
			if( Random.Range(0f, 100f) < 5 ){
				PrimaryFire();
			}
			if( Random.Range(0f, 100f) < 0.5 ){
				SecondaryFire();
			}
		}else{
			if ( canMove ){
				rigidBody.AddRelativeForce( new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) );
			}
			if ( Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") ){
				PrimaryFire();
			}
			if ( Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") ){
				SecondaryFire();
			}
		}
		ConstrainToWindow();
		IncrementCounters();
	}

	void PrimaryFire () {
		if (primaryFireCounter == primaryFireCooldown) {
			Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)) ,transform.position.z), transform.rotation );
			primaryFireCounter = 0;
		}
	}

	void SecondaryFire () {
		if (secondaryFireCounter == secondaryFireCooldown) {
			Instantiate(bomb, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)) ,transform.position.z), transform.rotation );
			secondaryFireCounter = 0;
		}
	}

	void ConstrainToWindow () {
		Vector3 tran = new Vector3();
		if(transform.position.x < -3.75f){
			rigidBody.velocity = new Vector2( Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}else if(transform.position.x > 3.75f){
			rigidBody.velocity = new Vector2( -Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}
	}

	void IncrementCounters() {
		if ( primaryFireCounter < primaryFireCooldown){
			primaryFireCounter++;
		}else{
			primaryFireCounter = primaryFireCounter;
		}
		if ( secondaryFireCounter < secondaryFireCooldown){
			secondaryFireCounter++;
		}else{
			secondaryFireCounter = secondaryFireCounter;
		}
	}

}
