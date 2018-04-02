using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class BallPhysicsManager : Singleton<BallPhysicsManager> {

	private Vector3 ballNetForceExperienced;
	private Vector3 ballCurrentVelocity;

	public Vector3 updatedVelocity = new Vector3(0, 0, 0);
	public Vector3 updatedForce = new Vector3(0, 0, 0);

	public Vector3 updatedVelocityBallTwo = new Vector3(0, 0, 0);
	public Vector3 updatedForceBallTwo = new Vector3(0, 0, 0);

	public GameObject ball;
	public GameObject ballTwo;

	public bool sceneHasTwoBalls;
	public bool isScene10 = false;
	public bool startStrobe = true;

    Vector3 initialPosition;
	Vector3 initialPositionBallTwo;


	// Use this for initialization
	void Start () {
		sceneHasTwoBalls = ballTwo != null && ballTwo.activeSelf;
	}

	// Update is called once per frame
	void Update () {

//        Debug.Log("Game Play Physiscs State: " + GameStateManager.Instance.currentPhysicsPlayState);
//		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
//			Rigidbody rbi = ball.GetComponent<Rigidbody> ();
//			rbi.isKinematic = false;
//			Debug.Log ("BallPhysicsManager update before velocity: " + rbi.velocity.magnitude);
//			rbi.velocity += updatedVelocity;
//			Debug.Log ("BallPhysicsManager update velocityupdate of the ball should be" + updatedVelocity);
//			Debug.Log ("BallPhysicsManager update after velocity: " + rbi.velocity.magnitude);
//			rbi.AddForce(updatedForce); 
//
//			if (sceneHasTwoBalls) {
//				Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody> ();
//				rbi.isKinematic = false;
//
//				rbiTwo.velocity += updatedVelocityBallTwo;
//				Debug.Log ("velocity of the ball should be" + updatedVelocity);
//				rbiTwo.AddForce(updatedForceBallTwo);
//			}
//        }
	}

	public void updateVelocity()
	{
		VelocityResponse.Instance.updateVelocity ();
		updatedVelocity = VelocityResponse.Instance.updatedVelocity;
		Debug.Log ("BallPhysicsManager update updatedVelocity: " + updatedVelocity);
		Debug.Log("value of ball two: " + ballTwo);
		if (ballTwo != null && ballTwo.activeSelf) {
			//            Debug.Log("ball two is not null");
			VelocityResponseBallTwo.Instance.updateVelocity ();
			updatedVelocityBallTwo= VelocityResponseBallTwo.Instance.updatedVelocity;
		}

		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			Rigidbody rbi = ball.GetComponent<Rigidbody> ();
			rbi.isKinematic = false;
			Debug.Log ("BallPhysicsManager update before velocity: " + rbi.velocity.magnitude);
			rbi.velocity += updatedVelocity;
			Debug.Log ("BallPhysicsManager update velocityupdate of the ball should be" + updatedVelocity);
			Debug.Log ("BallPhysicsManager update after velocity: " + rbi.velocity.magnitude);

			if (sceneHasTwoBalls) {
				Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody> ();
				rbiTwo.velocity += updatedVelocityBallTwo;
				Debug.Log ("velocity of the ball should be" + updatedVelocity);
			}
		}

	}

	public void updateForce()
	{
		ForceResponse.Instance.updateForce ();
		updatedForce = ForceResponse.Instance.updatedForce;
        if (ballTwo != null && ballTwo.activeSelf) {
			ForceResponseBallTwo.Instance.updateForce ();
			updatedForceBallTwo = ForceResponseBallTwo.Instance.updatedForce;
		}

		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			Rigidbody rbi = ball.GetComponent<Rigidbody> ();
			rbi.isKinematic = false;
			rbi.AddForce(updatedForce, ForceMode.Acceleration); 

			if (sceneHasTwoBalls) {
				Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody> ();
				rbiTwo.isKinematic = false;

				rbiTwo.AddForce(updatedForceBallTwo, ForceMode.Acceleration);
			}
		}

	}

    public void initBallPhysics () {
        initialPosition = ball.transform.position;
		ball.GetComponent<Rigidbody> ().velocity = updatedVelocity;
		ball.GetComponent<Rigidbody> ().AddForce (updatedForce);
		if (sceneHasTwoBalls) {
			initialPositionBallTwo = ballTwo.transform.position;
			ballTwo.GetComponent<Rigidbody> ().velocity = updatedForceBallTwo;
			ballTwo.GetComponent<Rigidbody> ().AddForce (updatedForceBallTwo);
		}

        GraphManager.Instance.startGraph();
    }

    public void stopBallPhysics() {
        GameStateManager.Instance.currentPhysicsPlayState = GameStateManager.GamePlayPhysicsState.OFF;
        
		ball.transform.Find ("Trail").gameObject.transform.parent = null;
		Rigidbody rbi = ball.GetComponent<Rigidbody>();
        rbi.velocity = new Vector3(0.0f, 0.0f, 0.0f);

		if (sceneHasTwoBalls) {
			ballTwo.transform.Find ("TrailTwo").gameObject.transform.parent = null;

			Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody>();
			rbiTwo.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}

		GraphManager.Instance.stopGraphRecording();
    }

    public void resetBall() {
        ball.transform.position = initialPosition;
		if (sceneHasTwoBalls) {
			ballTwo.transform.position = initialPositionBallTwo;
			ballTwo.GetComponent<StrobingHandler>().clearStrobes ();
		}
		ball.GetComponent<StrobingHandler>().clearStrobes ();
        stopBallPhysics();
    }
}
