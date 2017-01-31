using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	//Declare variables
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
		//set gloval rigidBody var to instance of this objects rigidbody component
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		//If We Are AI
		if ( isAI ){
			//add relative force to our object in a random range
			rigidBody.AddRelativeForce( new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 3.75f)) );
			//if a random roll of 1-100 is less than 5 (5% probability)
			if( Random.Range(0f, 100f) < 5 ){
				//fire primary weapon
				PrimaryFire();
			}
			//if a random roll of 1-100 is less than 0.5 (0.5% probability)
			if( Random.Range(0f, 100f) < 0.5 ){
				//fire secondary weapon
				SecondaryFire();
			}
		//If We Are Player
		}else{
			//if the canmove variable is set 
			if ( canMove ){
				//Add relative force equal to the horizontal and vertical getacis values.
				//This works for gamepads and keyboards alike.
				rigidBody.AddRelativeForce( new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) );
			}
			//if fire1 or jump
			if ( Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") ){
				//fire primary weapon
				PrimaryFire();
			}
			//if fire2 or fire3
			if ( Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") ){
				//fire secondary weapon
				SecondaryFire();
			}
		}
		//constrain movement of player to window edges
		ConstrainToWindow();
		//run any increment counters
		IncrementCounters();
	}

	void PrimaryFire () {
		//If the cooldown is up
		if (primaryFireCounter == primaryFireCooldown) {
			//Instantiate the bullet with a y offset to prefent collision with player
			Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)), transform.position.z), transform.rotation );
			//reset the cooldown counter
			primaryFireCounter = 0;
		}
	}

	void SecondaryFire () {
		//If the cooldown is up
		if (secondaryFireCounter == secondaryFireCooldown) {
			//Instantiate the bomb with a y offset to prefent collision with player
			Instantiate(bomb, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)), transform.position.z), transform.rotation );
			//reset the cooldown counter
			secondaryFireCounter = 0;
		}
	}

	void ConstrainToWindow () {
		//if the player goes too far left
		if(transform.position.x < -3.75f){
			//invert the rigidbodys x velocity, causing a bounce. We use absolute values to prevent this function getting looped and freezing a player at the edge
			rigidBody.velocity = new Vector2( Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}else if(transform.position.x > 3.75f){
			//invert the rigidbodys x velocity, causing a bounce. We use absolute values to prevent this function getting looped and freezing a player at the edge
			rigidBody.velocity = new Vector2( -Mathf.Abs(rigidBody.velocity.x*0.75f), 0 );
		}
	}

	void IncrementCounters() {
		//If the primary counter is less than the cooldown
		if ( primaryFireCounter < primaryFireCooldown){
			//increment it
			primaryFireCounter++;
		}
		//If the primary counter is less than the cooldown
		if ( secondaryFireCounter < secondaryFireCooldown){
			//increment it
			secondaryFireCounter++;
		}
	}

}
