using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using ChartAndGraph;


public class GraphManager : Singleton<GraphManager> {

	public const string VELOCITY = "Velocity";
	public const string FORCE = "Force";
//	public List<float> time = new List<float>();
//	public List<float> velocity = new List<float>();
	public List<float> force = new List<float>();
	public GraphChart Graph; 

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisplayGraph(){
//		if (Graph != null) {
//			Graph.DataSource.StartBatch ();
//			Graph.DataSource.ClearCategory (VELOCITY);
//			Graph.DataSource.ClearCategory (FORCE);
//
		}
	}

//	public void addnewValues(){
//		velocity.Add(BallPhysicsManager.Instance.updatedVelocity.magnitude);
//		force.Add (BallPhysicsManager.Instance.updatedForce.magnitude);
//	}

//	private void addtimeValues(){
//	BallPhysicsManager
//	}
//}


	