using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	//This is the list of Transform objects for the objectives
    public GameObject trashPrefab;
    public GameObject playerPrefab;

	private List<Transform> objectives = new List<Transform>();
    private List<Transform> players = new List<Transform>();

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
            GameObject justMade = Instantiate(trashPrefab, new Vector3(xPosition,0f,0f), Quaternion.identity);
            objectives.Add(justMade.transform);
        }
    }

    public void SpawnPlayer() {
        GameObject justMade = Instantiate(playerPrefab, new Vector3(0f,-3.75f,0f), Quaternion.identity);
		players.Add(justMade.transform);
    }

    public void SpawnEnemyAI() {
        GameObject justMade = Instantiate(playerPrefab, new Vector3(0f,3.75f,0f), Quaternion.Euler(0f, 0f, 180f));
        justMade.GetComponent<PlayerBehavior>().isAI = true;
        players.Add(justMade.transform);
    }

    public float DetermineObjectiveAverage() {
    	float average = 0;
    	for (int i = 0; i<objectives.Count; i++) {
			average += objectives[i].position.y;
    	}
    	return average /= objectives.Count;
    }

    public void DestroyAllGameObjects() {
		for (int i = objectives.Count-1; i>=0; i--) {
			Destroy( objectives[i].gameObject );
		}
		for (int i = players.Count-1; i>=0; i--) {
			Destroy( players[i].gameObject );
		}
		objectives.Clear();
		players.Clear();
    }
}