using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ForceResponse : Singleton<ForceResponse> {

	public List<GameObject> forces = new List<GameObject>();
	public Vector3 updatedForce = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addForceVector(GameObject gb)
	{
		forces.Add(gb);
		Debug.Log("New force added now there are " + forces.Count + " force vectors attached");
	}
	public void removeForceVector(GameObject gb)
	{
		forces.Remove(gb);
		Debug.Log("force removed now there are " + forces.Count + " force vectors attached");
	}
	public void updateForce() {
		Vector3 experiencedforce = new Vector3(0, 0, 0);

		//loop through all game objects that apply a force on the ball
		foreach (GameObject representationArrow in forces)
		{
			// get magnitude and direction of the current force affecting our object
			float magnitudeCurrentForceVector = representationArrow.transform.localScale.x*1000;
			Vector3 directionCurrentForceVector = - representationArrow.transform.up.normalized;

			//calculate the experienced force vector... and add it to the net Force
			Vector3 effective_force = magnitudeCurrentForceVector * directionCurrentForceVector;
			experiencedforce += effective_force;

		}
		updatedForce = experiencedforce;
	}

}
