using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {
	
	//The gamemode as n integer
	public int mode = 0;

	//A list of canvases which correspond with different gamemodes.
	public List<Canvas> canvases = new List<Canvas>();

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
		mode = newMode;
		for(int i = 0; i < canvases.Count; i++){
			canvases[i].enabled = (i == mode);
		}
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
