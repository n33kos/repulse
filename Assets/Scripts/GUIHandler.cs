using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GUIHandler : NetworkBehaviour {
	
	//The gamemode as n integer
	[SyncVar]
	public int mode = 0;

	//A list of canvases which correspond with different gamemodes - set in the editor
	public List<Canvas> canvases = new List<Canvas>();

	private GameState gameState;

	void Start () {
		gameState = GameObject.Find("GameState").GetComponent<GameState>();
	}

	void OnGUI () {
		switch(mode){
			case 0:
				drawTitleScreen();
				break;
			case 1:
				drawPlayScreen();
				break;
			case 2:
				drawScoreScreen();
				break;
			default:
				//do nothing
				break;
		}
	}

	public void SetGuiMode (int newMode) {
		//set global variable mode equal to newMode
		mode = newMode;
		//iterate through canvas objects
		for(int i = 0; i < canvases.Count; i++){
			//set all canvases disabled except for the one which corresponds with our mode integer
			canvases[i].enabled = (i == mode);
		}

		//Reset isplaying variable when we enter gamemode 1 if it is currently set to false
		if( gameState.isPlaying == false && newMode == 1) 
			gameState.isPlaying = true;
	}

	void drawTitleScreen () {
		//Debug.Log("Title Screen");
	}

	void drawPlayScreen () {
		//Debug.Log("Play Screen");
	}

	void drawScoreScreen () {
		//Debug.Log("Score Screen");
	}

}
