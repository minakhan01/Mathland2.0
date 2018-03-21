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

    Vector3 initialPosition;


	// Use this for initialization
	void Start () {

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
            Debug.Log("ball two is not null");
			ForceResponseBallTwo.Instance.updateForce ();
			VelocityResponseBallTwo.Instance.updateVelocity ();
			updatedVelocityBallTwo = ForceResponseBallTwo.Instance.updatedForce;
			updatedForceBallTwo = VelocityResponseBallTwo.Instance.updatedVelocity;
		}

	}

    public void initBallPhysics () {
        initialPosition = ball.transform.position;
        GraphManager.Instance.startGraph();
    }

    public void resetBall() {
        ball.transform.position = initialPosition;
        Rigidbody rbi = ball.GetComponent<Rigidbody>();
        rbi.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        GraphManager.Instance.stopGraph();
		StrobingHandler.Instance.clearStrobes ();
    }
}
