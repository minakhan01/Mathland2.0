﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityResponse : Singleton<VelocityResponse> {

	public List<GameObject> velocities = new List<GameObject>();
	//    public GameObject fullScaledforce;
	public Vector3 updatedVelocity = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void addVelocityVector(GameObject gb)
	{
		velocities.Add(gb);
		Debug.Log("Mina Debug VelocityResponse New velocity added now there are " + velocities.Count + " velocity vectors attached");
	}
	public void removeVelocityVector(GameObject gb)
	{
		velocities.Remove(gb);
		Debug.Log("Mina Debug VelocityResponse velocity removed now there are " + velocities.Count + " velocity vectors attached");
	}

	public void updateVelocity() {
		//initialize the total velocity and experienced force which represents
		//ball's vel / force after all game tool interactions

		Vector3 objectInitVelocity = new Vector3(0, 0, 0);

		//start by looping through all game objects that contribute to velocity
		foreach (GameObject velocityAffectingGameObject in velocities)
		{
			//get the magnitude of velocity arrow
			float magnitudeCurrentForceVelocity = velocityAffectingGameObject.transform.localScale.x*500; 

			//impose this magnitude on the direction of the arrow
			Vector3 VelocityVector = - magnitudeCurrentForceVelocity * 
				velocityAffectingGameObject.transform.up.normalized;

			//add this to the ball's total velocity
			objectInitVelocity += VelocityVector;
		}
		velocities = new List<GameObject> ();
		//set our gameobject's initial velocity to be the total velocity of gameobjects acting on it
		updatedVelocity=objectInitVelocity;
	}
}
