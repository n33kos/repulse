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
}
