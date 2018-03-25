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

    Vector3 initialPosition;
	Vector3 initialPositionBallTwo;


	// Use this for initialization
	void Start () {
		sceneHasTwoBalls = ballTwo != null && ballTwo.activeSelf;
	}

	// Update is called once per frame
	void Update () {
        Debug.Log("Game Play Physiscs State: " + GameStateManager.Instance.currentPhysicsPlayState);
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			Rigidbody rbi = ball.GetComponent<Rigidbody> ();
			rbi.isKinematic = false;

			rbi.velocity += updatedVelocity;
			Debug.Log ("velocity of the ball should be" + updatedVelocity);
			rbi.AddForce(updatedForce); 

			if (sceneHasTwoBalls) {
				Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody> ();
				rbi.isKinematic = false;

				rbiTwo.velocity += updatedVelocityBallTwo;
				Debug.Log ("velocity of the ball should be" + updatedVelocity);
				rbiTwo.AddForce(updatedForceBallTwo);
			}
        }
	}


	public void updateVelocityandForce()
	{
		ForceResponse.Instance.updateForce ();
		VelocityResponse.Instance.updateVelocity ();
		updatedForce = ForceResponse.Instance.updatedForce;
		updatedVelocity = VelocityResponse.Instance.updatedVelocity;

        Debug.Log("value of ball two: " + ballTwo);
        if (ballTwo != null && ballTwo.activeSelf) {
//            Debug.Log("ball two is not null");
			ForceResponseBallTwo.Instance.updateForce ();
			VelocityResponseBallTwo.Instance.updateVelocity ();
			updatedForceBallTwo = ForceResponseBallTwo.Instance.updatedForce;
			updatedVelocityBallTwo= VelocityResponseBallTwo.Instance.updatedVelocity;
		}

	}

    public void initBallPhysics () {
        initialPosition = ball.transform.position;

		if (sceneHasTwoBalls) {
			initialPositionBallTwo = ballTwo.transform.position;
		}

        GraphManager.Instance.startGraph();
    }

    public void stopBallPhysics() {
        GameStateManager.Instance.currentPhysicsPlayState = GameStateManager.GamePlayPhysicsState.OFF;
        
		Rigidbody rbi = ball.GetComponent<Rigidbody>();
        rbi.velocity = new Vector3(0.0f, 0.0f, 0.0f);

		if (sceneHasTwoBalls) {
			Rigidbody rbiTwo = ballTwo.GetComponent<Rigidbody>();
			rbiTwo.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}

        GraphManager.Instance.stopGraph();
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
