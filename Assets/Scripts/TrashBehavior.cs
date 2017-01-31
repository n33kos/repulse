using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour {
    //Referrence the GameState object
    public GameObject gameStateObject;
    public GameState gameState;
    
    // Set initial Trash position
    public float targetPosY = 0;

    // Use this for initialization
    void Start () {
        gameStateObject = GameObject.Find("GameState");
        gameState = gameStateObject.GetComponent<GameState>();
    }
	
	void FixedUpdate () {
        if (gameState.isPlaying == true)
        {
            //every FixedUpdate frame, set y position to its current position + the target position divided by the movement speed
            //then we multiply that by Time.fixedDeltaTime to keep it smooth as the time values increment.
            transform.position = new Vector3(transform.position.x, transform.position.y + targetPosY/1.25f*Time.fixedDeltaTime, 0);
            
            //Check if the current player has scored or the enemy player has scored        
            if (transform.position.y > 4f)
            {
                // Add a point to the player's score
                gameState.setScore(true);
                Destroy(gameObject);
            }
            else if (transform.position.y < -4f)
            {
                // Add a point to the enemies score
                gameState.setScore(false);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameState.isPlaying == true)
        {
            //Get the bullet behavior script
            BulletBehavior bb = col.transform.GetComponent<BulletBehavior>();
            //if the collider doesnt have one, it wasnt a bullet and we need to return early
            if (!bb)
                return;
            
            //get bullet power and position
            float bulletPower = bb.power / 10;
            float bulletPosition = col.transform.position.y;

            //if bullet position is greater than trash position
            if (bulletPosition > transform.position.y)
            {
                //set target y position lower than it was
                targetPosY = targetPosY - bulletPower;
            }
            else
            {
                //set target y position higher than it was
                targetPosY = targetPosY + bulletPower;
            }
        }
    }
}
