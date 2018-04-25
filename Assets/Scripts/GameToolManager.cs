using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using Lean.Touch;

public class GameToolManager : Singleton<GameToolManager> {

	public GameObject ForceField, VelocityVector, Rope, Cube, Ramp,ball;
	private List<GameObject> gameToolList = new List<GameObject> ();

    public LeanSelect leanSelect;

	public void CreateForceField() {
		GameObject ff = CreateGameToolPrefab (ForceField);
        leanSelect.Select(null, ff.GetComponent<LeanSelectable>());
	}

	public void CreateVelocityVector() {
		GameObject velocityVector = (GameObject) CreateGameToolPrefab (VelocityVector);
//		velocityVector.GetComponent<VelocityTrigger> ().BALL = ball;

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

	private GameObject CreateGameToolPrefab(GameObject prefab){
		GameObject gb=Instantiate (prefab, new Vector3(1,0,5),Quaternion.identity) as GameObject;
		gameToolList.Add (gb);
        Debug.Log ("GAME TOOL MANAGER - Game Tool List: " + gameToolList);
		//Debug.Log(gameToolList);
		return gb;
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
