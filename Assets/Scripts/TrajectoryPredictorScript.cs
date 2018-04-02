using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPredictorScript : MonoBehaviour
{
	//create a trajectory predictor in code
	TrajectoryPredictor tp;
	public Vector3 force_exp = new Vector3 (0, 0, 0);
	public Vector3 velocity_of_ball = new Vector3 (0, 0, 0);
	public GameObject objToLaunch;
	public Transform launchPoint;

	// Use this for initialization
	void Start ()
	{
		tp = objToLaunch.GetComponent<TrajectoryPredictor> ();
		tp.drawDebugOnPrediction = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		launchPoint.transform.position = objToLaunch.transform.position;
		launchPoint.transform.rotation = objToLaunch.transform.rotation;
        
	}

	public void resetBall ()
	{
		force_exp = new Vector3 (0, 0, 0);
		velocity_of_ball = new Vector3 (0, 0, 0);
	}


	void LateUpdate ()
	{
		//if (BallStateManager.Instance.currentBallState == BallStateManager.BallState.Grabbed)
		{
			
			//dubug purposes, make sure this is attached to a gameobject
			if (objToLaunch == null) {
				throw new MissingComponentException ("No gameobject is attached to this trajectory predictor");
			}

			// if we have a velocityReactor component, update its initVel / experiencedForce
//			BallPhysicsManager.Instance.updateVelocityandForce ();

			if (gameObject.name == BallPhysicsManager.Instance.ball.name) {
				velocity_of_ball = BallPhysicsManager.Instance.updatedVelocity;
				force_exp = BallPhysicsManager.Instance.updatedForce;
			}
			else if (gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
				velocity_of_ball = BallPhysicsManager.Instance.updatedVelocityBallTwo;
				force_exp = BallPhysicsManager.Instance.updatedForceBallTwo;
			}
			Debug.Log ("Velocity of ball is " + velocity_of_ball + " and force is " + force_exp);

			/*********** Here we work with the trajectory predictor to draw the correct line ********/
			tp.debugLineDuration = Time.unscaledDeltaTime;


			//tell the predictor to predict a 3d line. this will also cause it to draw a prediction line
			//because drawDebugOnPredict is set to true
			tp.Predict3D (launchPoint.position, velocity_of_ball, force_exp);

		}

	}
}
