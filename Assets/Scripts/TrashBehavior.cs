using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour {

    public float xPos;
    float yPos = 0f;


    // Use this for initialization
    void Start () {
        setPosition(transform.position.x, yPos);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPosition( float xPos, float yPos) {
        transform.position = new Vector3(xPos, yPos, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        float bulletPower = col.transform.GetComponent<BulletBehavior>().power/5;
        float bulletPosition = col.transform.position.y;
        float positionY;

        if (bulletPosition > transform.position.y)
        {
            positionY = transform.position.y - bulletPower;
        } else
        {
            positionY = transform.position.y + bulletPower;
        }

        setPosition(transform.position.x, positionY);
    }
}
