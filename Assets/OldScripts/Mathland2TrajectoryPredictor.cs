using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mathland2TrajectoryPredictor : MonoBehaviour {
	//create a trajectory predictor in code
	TrajectoryPredictor tp;
	Rigidbody rb;
	public GameObject BallBeingPredicted;


	// Use this for initialization
	void Start () {
		tp = BallBeingPredicted.GetComponent<TrajectoryPredictor>();
		rb = BallBeingPredicted.GetComponent<Rigidbody>();
		tp.drawDebugOnPrediction = true;
	}
	
	// Update is called once per frame
	void Update () {
		tp.Predict3D (rb);
	}
}
