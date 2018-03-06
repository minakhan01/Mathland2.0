using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class GameToolManager : Singleton<GameToolManager> {

	public GameObject ForceField, VelocityVector, Rope, Cube, Ramp;
	private List<GameObject> gameToolList = new List<GameObject> ();

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
		GameObject gb=Instantiate (prefab, new Vector3(1,0,5),Quaternion.identity) as GameObject;
		gameToolList.Add (gb);
		Debug.Log ("The list now is");
		Debug.Log(gameToolList);
	}

	public void DestroyGameTool(GameObject gb){
		GameObject.Destroy (gb);
		if(gameToolList.Contains(gb))gameToolList.Remove (gb);
	}

	public void DestroyObject()
	{
        
		Destroy (SelectedObjectManager.Instance.selectedObject);
        if (gameToolList.Contains(SelectedObjectManager.Instance.selectedObject)) gameToolList.Remove(SelectedObjectManager.Instance.selectedObject);
		SelectedObjectManager.Instance.selectedObject = null;
	}

	public void DestroyAllGameTools(){
		foreach (GameObject gb in gameToolList) {
			gameToolList.Remove (gb);
			GameObject.Destroy (gb);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("The script started");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
