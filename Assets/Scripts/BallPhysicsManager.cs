using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class BallPhysicsManager : Singleton<BallPhysicsManager> {

	private Vector3 ballNetForceExperienced;
	private Vector3 ballCurrentVelocity;

	public Vector3 updatedVelocity = new Vector3(0, 0, 0);
	public Vector3 updatedForce = new Vector3(0, 0, 0);

	public GameObject ball;

    Vector3 initialPosition;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
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
