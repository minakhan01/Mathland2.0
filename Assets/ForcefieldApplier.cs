using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForcefieldApplier : MonoBehaviour {
	public GameObject siblingarrow;
	public Vector3 updateExperiencedForce = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider collidee)

	{
		if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
		{
			collidee.gameObject.GetComponent<ForceResponse>().addForceVector(siblingarrow);
			//AffectedObjects.Add(collidee.gameObject);
			Debug.Log("Thing entered ForceField");
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.blue;
		}
	}
	void OnTriggerExit(Collider collidee)
	{
		if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
		{
			collidee.gameObject.GetComponent<ForceResponse>().removeForceVector(siblingarrow);
			//AffectedObjects.Remove(collidee.gameObject);
			//collidee.gameObject.GetComponent<VelocityReactor>().experiencedforce = new Vector3(0, 0, 0);
			Debug.Log("Thing escaped ForceField");
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.red;
		}
	}
	void OnDestroy(){
		Destroy (siblingarrow); 
	}
}
