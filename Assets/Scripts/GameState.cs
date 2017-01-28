using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	//This is the list of Transform objects for the objectives
	public List<Transform> objectives = new List<Transform>();
    public GameObject trash;
    public GameObject player;

	void Start () {
        //SpawnTrash();
        //SpawnPlayer();
        //SpawnEnemyAI();
	}
	
	void Update () {
		
	}

	void LateUpdate () {
	}

	public void ExitGame () {
		Application.Quit();
	}

    public void SpawnTrash () {
        // Set first trash spawn position
        float xPosition = -4.25f;
        float xPositionOffset = 0.50f;
        for (int i = 0; i < 16; i++) {
            //Set correct spawn position
            xPosition += xPositionOffset;
            //Spawn a trash object
            GameObject justMade = Instantiate(trash, new Vector3(xPosition,0f,0f), Quaternion.identity);
            objectives.Add(justMade.transform);
        }
    }

    public void SpawnPlayer(){
        Instantiate(player, new Vector3(0f,-3.75f,0f), Quaternion.identity);
    }

    public void SpawnEnemyAI(){
        GameObject justMade = Instantiate(player, new Vector3(0f,3.75f,0f), Quaternion.Euler(0f, 0f, 180f));
        justMade.GetComponent<PlayerBehavior>().isAI = true;
    }

    public float DetermineObjectiveAverage(){
    	float average = 0;
    	for (int i = 0; i<objectives.Count; i++) {
			average += objectives[i].position.y;
    	}
    	return average /= objectives.Count;
    }
}