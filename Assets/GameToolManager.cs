using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class GameToolManager : Singleton<GameToolManager> {

	public GameObject ForceField, VelocityVector, Rope, Cube, Ramp;
	private List<GameObject> gameToolList;

	public void CreateForceField() {
		CreateGameToolPrefab (ForceField);
	}

	public void CreateVelocityVector() {
		CreateGameToolPrefab (VelocityVector);
	}

	public void CreateRope() {
		CreateGameToolPrefab (Rope);
	}

	public void CreateCube() {
		CreateGameToolPrefab (Cube);
	}


	public void CreateRamp() {
		CreateGameToolPrefab (Ramp);
	}

	private void CreateGameToolPrefab(GameObject prefab){
		GameObject gb=Instantiate (prefab, new Vector3(1,0,5),Quaternion.identity);
		gameToolList.Add (gb);
	}
	public void DestroyGameTool(GameObject gb){
		GameObject.Destroy (gb);
		if(gameToolList.Contains(gb))gameToolList.Remove (gb);
	}

	public void DestroyAllGameTools(){
		foreach (GameObject gb in gameToolList) {
			GameObject.Destroy (gb);
			gameToolList.Remove (gb);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
