using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour {
	public GUIHandler guiHandler;

	public float targetPosY = 0;

	// Use this for initialization
	void Start () {
		guiHandler = GameObject.Find("GameState").GetComponent<GUIHandler>();
	}
	
	void FixedUpdate () {
		//every FixedUpdate frame, set y position to its current position + the target position divided by the movement speed
		//then we multiply that by Time.fixedDeltaTime to keep it smooth as the time values increment.
		transform.position = new Vector3(transform.position.x, transform.position.y + targetPosY/1.25f*Time.fixedDeltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D col)
	{   
		//Get the bullet behavior script
		BulletBehavior bb = col.transform.GetComponent<BulletBehavior>();
		//if the collider doesnt have one, it wasnt a bullet and we need to return early
		if (!bb)
			return;
		
		//get bullet power and position
		float bulletPower = bb.power/10;
		float bulletPosition = col.transform.position.y;

		//if bullet position is greater than trash position
		if (bulletPosition > transform.position.y)
		{
			//set target y position lower than it was
			targetPosY = targetPosY - bulletPower;
		} else {
			//set target y position higher than it was
			targetPosY = targetPosY + bulletPower;
		}

		// You WIN!!!!
		if (targetPosY > 4f)
		{
			Debug.Log("Win");
			guiHandler.SetGuiMode(2);
		} else if (targetPosY < -4f)
		{
			Debug.Log("Fail");
			guiHandler.SetGuiMode(2);
		}
	}
}
