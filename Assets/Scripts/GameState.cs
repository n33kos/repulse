using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	//This is the list of Transform objects for the objectives
	public List<Transform> objectives = new List<Transform>();
    public GameObject trash;

	void Start () {
        SpawnTrash();
	}
	
	void Update () {
		
	}

	public void ExitGame () {
		Application.Quit();
	}

    public void SpawnTrash () {
        // Set first trash spawn position
        float xPosition = -4.25f;
        float xPositionOffset = 0.50f;
        for (int i = 0; i < 16; i++) {
            //Set correct spwan position
            xPosition += xPositionOffset;
            //Spawn a trash object
            Instantiate(trash, new Vector3(xPosition,0f,0f), Quaternion.identity);
        }
    }
}