using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using ChartAndGraph;


public class GraphManager : Singleton<GraphManager> {

	public const string VELOCITY = "Velocity";
	public const string FORCE = "Force";
	public GraphChart Graph;
	float lastTime = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//get time in seconds since start of game
		float time = Time.time;
		//check to see that 2 seconds have passed
		if (lastTime + 2f < time)
		{
			lastTime = time;
			//add new (x,y) points to graph--one for VELOCITY and one for FORCE
//			Graph.DataSource.AddPointToCategoryRealtime(VELOCITY, System.DateTime.Now, BallPhysicsManager.Instance.updatedVelocity.magnitude); // each time we call AddPointToCategory 
//			Graph.DataSource.AddPointToCategoryRealtime(FORCE, System.DateTime.Now, BallPhysicsManager.Instance.updatedForce.magnitude); // each time we call AddPointToCategory
			Graph.DataSource.AddPointToCategoryRealtime(VELOCITY, time, BallPhysicsManager.Instance.updatedVelocity.magnitude); // each time we call AddPointToCategory 
			Graph.DataSource.AddPointToCategoryRealtime(FORCE, time, BallPhysicsManager.Instance.updatedForce.magnitude); // each time we call AddPointToCategory
		}
	}
	//hide/show display
	public void toggleDisplay(){
		}
	}


	