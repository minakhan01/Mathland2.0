﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;


public class BallMotionManager : MonoBehaviour {

    private Vector3 ballNetForceExperienced;
    private Vector3 ballCurrentVelocity;

	public Vector3 updatedVelocity = new Vector3(0, 0, 0);
	public Vector3 updatedForce = new Vector3(0, 0, 0);


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {

			Rigidbody rbi = GetComponent<Rigidbody> ();
			rbi.isKinematic = false;
			rbi.velocity += updatedVelocity;
			Debug.Log ("velocity of the ball should be" + updatedVelocity);
			rbi.AddForce(updatedForce); 
		}
	}


	public void updateVelocityandForce()
	{
		ForceResponse.Instance.updateForce ();
		VelocityResponse.Instance.updateVelocity ();
		updatedForce = ForceResponse.Instance.updatedForce;
		updatedVelocity = VelocityResponse.Instance.updatedVelocity;

	}


}
