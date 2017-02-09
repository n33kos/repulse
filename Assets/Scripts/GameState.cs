using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameState : NetworkBehaviour {
    public GUIHandler guiHandler;

    //Public prefabs to be set in the editor
    public GameObject trashPrefab;
    public GameObject playerPrefab;
    //This is the list of Transform objects for the objectives
    public List<Transform> objectives = new List<Transform>();
    //This is the list of Transform objects for the players
    public List<Transform> players = new List<Transform>();

    // Score variables
    public int playerScore = 0;
    public int enemyScore = 0;

    // Set if is playing or not
    public bool isPlaying = true;

	void Start () {
        //CmdSpawnTrash();
        //SpawnPlayer();
        //SpawnEnemyAI();
        guiHandler = GetComponent<GUIHandler>();
    }

	public void ExitGame () {
        //Make a public function available to buttons which closes the application
		Application.Quit();
	}

    [Command]
    public void CmdSpawnTrash () {
        // Set first trash spawn position
        float xPosition = -4.25f;
        // Set x position offset
        float xPositionOffset = 0.50f;
        for (int i = 0; i < 16; i++) {
            //Set correct spawn position
            xPosition += xPositionOffset;
            //Spawn a trash object and set it to a temporary variable
            GameObject justMade = Instantiate(trashPrefab, new Vector3(xPosition,0f,0f), Quaternion.identity);
            //Add the just created object to the list of objectives so we can find it later
            objectives.Add(justMade.transform);
            //send the signal for the server to spawn for all clients
            NetworkServer.Spawn(justMade);
        }
    }

    public void SpawnPlayer() {
        //Spawn a player object and set it to a temporary variable
        GameObject justMade = Instantiate(playerPrefab, new Vector3(0f,-3.75f,0f), Quaternion.identity);
        //Add the just created object to the list of players
		players.Add(justMade.transform);
    }

    public void SpawnEnemyAI() {
        //Spawn a player object and set it to a temporary variable
        GameObject justMade = Instantiate(playerPrefab, new Vector3(0f,3.75f,0f), Quaternion.Euler(0f, 0f, 180f));
        //Set the isAI value of the jsut created object to true
        justMade.GetComponent<PlayerBehavior>().isAI = true;
        //Add the just created object to the list of players
        players.Add(justMade.transform);
    }

    public float DetermineObjectiveAverage() {
        //Init average float
    	float average = 0;
    	for (int i = 0; i<objectives.Count; i++) {
            //Add the values
			average += objectives[i].position.y;
    	}
        //return the average by dividing the total by the count
    	return average /= objectives.Count;
    }

    public void UnlistObjective(GameObject condemned) {
        //iterate through objective gameobjects
        for (int i = objectives.Count-1; i>=0; i--) {
            if (objectives[i].gameObject.GetInstanceID() == condemned.GetInstanceID())
            {
                //destroy objective gameobject
                objectives.RemoveAt(i);
            }
        }        
    }

    public void DestroyAllGameObjects() {
        //iterate through objective gameobjects
        if (objectives.Count > 0) {
    		for (int i = objectives.Count-1; i>=0; i--) {
                if (objectives[i].gameObject != null)
                {
                    //destroy objective gameobject
                    Destroy(objectives[i].gameObject);
                }
    		}
        }
        //iterate through player gameobjects
        if ( players.Count > 0) {
    		for (int i = players.Count-1; i>=0; i--) {
                if (players[i].gameObject != null)
                {
                    //destroy player gameobject
                    Destroy(players[i].gameObject);
                }
    		}
        }
        //empty both lists so we dont reference deleted objects.
		objectives.Clear();
		players.Clear();
    }

    public void setScore(bool player)
    {
        if (player == true)
        {
            playerScore = addToPlayerScore();
        }
        else
        {
            enemyScore = addToEnemyScore();
        }

        if (playerScore > 4)
        {
            victoryOrDefeat(true);
        } else if (enemyScore > 4)
        {
            victoryOrDefeat(false);
        }
    }

    public int addToPlayerScore()
    {
        playerScore ++;
        Debug.Log("Player Score: " + playerScore);
        return playerScore; 
    }

    public int addToEnemyScore()
    {
        enemyScore ++;
        Debug.Log("Enemy Score: " + enemyScore);
        return enemyScore;
    }

    public void victoryOrDefeat(bool victory)
    {
        isPlaying = false;
        if (victory == true)
        {
            Debug.Log("You've Won");
            guiHandler.SetGuiMode(2);
            DestroyAllGameObjects();
        } else
        {
            Debug.Log("You've Lost");
            guiHandler.SetGuiMode(2);
            DestroyAllGameObjects();
        }
    }
}