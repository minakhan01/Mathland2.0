using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColtonTrajectPredictorScript : MonoBehaviour {

	//trajectory predictor stuff
	TrajectoryPredictor tp;
	public Transform launchOrigin;

	//game obejct stuff
	public GameObject ball;
	public Vector3 currentForce= new Vector3(0, 0, 0);
	public Vector3 currentVelocity = new Vector3(0, 0, 0);


	//initialize trajectory predictor
	void Start () {
		tp = ball.GetComponent<TrajectoryPredictor>();
		tp.drawDebugOnPrediction = true;
	}


	// get launch origin info from ball
	void Update () {
		launchOrigin.transform.position = ball.transform.position;
		launchOrigin.transform.rotation = ball.transform.rotation;
	}


	void LateUpdate()
	{
//		//set velocity to be the ball's velocity
//		currentVelocity = ball.GetComponent<Rigidbody>().velocity;
//
//
//		// if we have a velocityReactor component, update its initVel / experiencedForce
//		ball.GetComponent<VelocityReactor>().updateVelocityandForce();
//		currentVelocity = ball.GetComponent<VelocityReactor>().objectInitVelocity;
//		currentForce = ball.GetComponent<VelocityReactor>().experiencedForce;


		/*********** Here we work with the trajectory predictor to draw the correct line ********/
		tp.Predict3D(launchOrigin.position, currentVelocity , currentForce);
		Debug.Log (launchOrigin.position);
		Debug.Log (currentVelocity);
		Debug.Log (currentForce);
		/******************* End Trajectory Predictor ****************/

	}
}
