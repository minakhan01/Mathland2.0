using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTrigger : MonoBehaviour {

	public GameObject mainBall;
	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter(Collider collidee)
	{
		gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		if (isABall (collidee)) {
			RopeResponse.Instance.addBallRopeConnnection();
		}

		Debug.Log ("Rope trigger entered");
	}

	private void OnTriggerExit(Collider collidee)
	{
		gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
		if (isABall (collidee)) {
			RopeResponse.Instance.removeBallRopeConnnection();
		}
		Debug.Log ("Rope trigger exited");
	}

	// Update is called once per frame
	void Update () {
		
	}

	bool isABall (Collider collidee) {
		return (collidee.gameObject.name == mainBall.name);        
	}
}
