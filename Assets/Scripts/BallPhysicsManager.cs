using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using Obi;

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

	public GameObject rope;


	// Use this for initialization
	void Start () {
		sceneHasTwoBalls = ballTwo != null && ballTwo.activeSelf;
	}

	public void breakBallFromRope() {
	 	if (rope != null) {
			// Pin both ends of the rope (this enables two-way interaction between character and rope):
			ObiPinConstraints pinConstraints = rope.GetComponent<ObiPinConstraints>();
			pinConstraints.RemoveFromSolver(null);
			ForceArrowManager.Instance.showRopeForce = false;
		}
	}

	// Update is called once per frame
	void Update () {
        Debug.Log("Game Play Physiscs State: " + GameStateManager.Instance.currentPhysicsPlayState);
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			updateVelocityandForce ();
			Rigidbody rbi = ball.GetComponent<Rigidbody> ();
			rbi.isKinematic = false;
			Debug.Log ("Mina Debug BallPhysicsManager update before velocity " + rbi.velocity );
			Debug.Log ("Mina Debug BallPhysicsManager updatedVelocity " + updatedVelocity );
			rbi.velocity += updatedVelocity;
			Debug.Log ("Mina Debug BallPhysicsManager update after velocity " + rbi.velocity );
			Debug.Log ("Mina Debug BallPhysicsManager updatedForce " + updatedForce );
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
		Debug.Log ("Mina Debug BallPhysicsManager before updateVelocityandForce updatedForce: "+updatedForce+" updatedVelocity: "+updatedVelocity);
		ForceResponse.Instance.updateForce ();
		VelocityResponse.Instance.updateVelocity ();
		updatedForce = ForceResponse.Instance.updatedForce;
		updatedVelocity = VelocityResponse.Instance.updatedVelocity;
		Debug.Log ("Mina Debug BallPhysicsManager after updateVelocityandForce updatedForce: "+updatedForce+" updatedVelocity: "+updatedVelocity);

        Debug.Log("value of ball two: " + ballTwo);
        if (ballTwo != null && ballTwo.activeSelf) {
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

		if (!isScene10) {
			GraphManager.Instance.startGraph ();
		}
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
