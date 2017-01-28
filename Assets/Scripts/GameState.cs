using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	//This is the list of Transform objects for the objectives
	public List<Transform> objectives = new List<Transform>();

	void Start () {
	}
	
	void Update () {
		
	}

	public void ExitGame () {
		Application.Quit();
	}
}