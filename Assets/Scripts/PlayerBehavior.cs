using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

///Inherit from networkBehavior Instead of Monobehavior
public class PlayerBehavior : NetworkBehaviour {

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

	private int controlDirection = 1;

	void Start () {
		//set global rigidBody var to instance of this objects rigidbody component
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}

    //Do things to customize the local player avatar
    public override void OnStartLocalPlayer() {
        GetComponent<SpriteRenderer>().color = Color.white;
        //if we are the first connected player, turn the camera 180 degrees
		if (NetworkServer.connections.Count == 1){
			Camera.main.transform.eulerAngles = new Vector3(0f, 0f, 180f);
			//we also reverse the control direction to a negative number, flipping the controls
			controlDirection = -1;
		}
    }
	
	void FixedUpdate () {

		//If We Are AI
		if ( isAI ){
			//add relative force to our object in a random range
			rigidBody.AddRelativeForce( new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 3.75f)) );
			//if a random roll of 1-100 is less than 5 (5% probability)
			if( Random.Range(0f, 100f) < 5 ){
				//fire primary weapon
				CmdPrimaryFire();
			}
			//if a random roll of 1-100 is less than 0.5 (0.5% probability)
			if( Random.Range(0f, 100f) < 0.5 ){
				//fire secondary weapon
				CmdSecondaryFire();
			}
		//If We Are the local Player and not AI
		}else if(isLocalPlayer && !isAI){
			//if the canmove variable is set 
			if ( canMove ){
				//Add relative force equal to the horizontal and vertical getacis values.
				//This works for gamepads and keyboards alike.
				rigidBody.AddRelativeForce( new Vector2(Input.GetAxis("Horizontal")*controlDirection, 0) );
			}
			//if fire1 or jump
			if ( Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") ){
				//fire primary weapon
				CmdPrimaryFire();
			}
			//if fire2 or fire3
			if ( Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") ){
				//fire secondary weapon
				CmdSecondaryFire();
			}
		}
		//constrain movement of player to window edges
		ConstrainToWindow();
		//run any increment counters
		IncrementCounters();
	}
	
	[Command]
	void CmdPrimaryFire () {
		//If the cooldown is up
		if (primaryFireCounter == primaryFireCooldown) {
			//Instantiate the bullet with a y offset to prefent collision with player
			GameObject justmade = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)), transform.position.z), transform.rotation );
			//reset the cooldown counter
			primaryFireCounter = 0;
			//Send the command for teh server to spawn the bullet on all clients
			NetworkServer.Spawn(justmade);
		}
	}
	
	[Command]
	void CmdSecondaryFire () {
		//If the cooldown is up
		if (secondaryFireCounter == secondaryFireCooldown) {
			//Instantiate the bomb with a y offset to prefent collision with player
			GameObject justmade = Instantiate(bomb, new Vector3(transform.position.x, transform.position.y+(transform.up.y*(transform.localScale.y/2)), transform.position.z), transform.rotation );
			//reset the cooldown counter
			secondaryFireCounter = 0;
			//Send the command for teh server to spawn the bullet on all clients
			NetworkServer.Spawn(justmade);
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