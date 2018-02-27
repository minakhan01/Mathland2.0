using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class GameToolManager : Singleton<GameToolManager> {

	public GameObject ForceField, VelocityVector, Rope, Cube, Ramp;
	public void createForceField() {
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
		Instantiate (prefab, new Vector3(1,0,5),Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
