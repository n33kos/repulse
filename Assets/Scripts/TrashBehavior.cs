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
	   transform.position = new Vector3(transform.position.x, transform.position.y + targetPosY/1.25f*Time.fixedDeltaTime, 0);
	}

    void OnTriggerEnter2D(Collider2D col)
    {   
        BulletBehavior bb = col.transform.GetComponent<BulletBehavior>();
        if (!bb)
            return;
            
        float bulletPower = bb.power/10;
        float bulletPosition = col.transform.position.y;

        if (bulletPosition > transform.position.y)
        {
            targetPosY = targetPosY - bulletPower;
        } else {
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
