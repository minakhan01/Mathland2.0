using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTrigger : MonoBehaviour {
    public Vector3 VelocityVector=new Vector3(0,0,0);
    public float VelocityVectorMagnitude=1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collidee)
    {
		
		Debug.Log ("BallPhysicsManager update OnTriggerEnter velocity");
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.green;
		//GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Active");
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			BallPhysicsManager.Instance.ball.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount++;
			int count = BallPhysicsManager.Instance.ball.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount;
			if (count == 1) {
				VelocityResponse.Instance.addVelocityVector (gameObject);
				Debug.Log ("BallPhysicsManager update VelocityVector and ball one collided!");
				BallPhysicsManager.Instance.updateVelocity ();
			}
		} else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			BallPhysicsManager.Instance.ballTwo.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount++;
			int count = BallPhysicsManager.Instance.ballTwo.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount;
			if (count == 1) {
				VelocityResponseBallTwo.Instance.addVelocityVector (gameObject);
				Debug.Log ("VelocityVector and ball two collided!");
				BallPhysicsManager.Instance.updateVelocity ();
			}
		}
		

    }
    private void OnTriggerExit(Collider collidee)
    {
		Debug.Log ("BallPhysicsManager update OnTriggerEnter exit");

		gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			BallPhysicsManager.Instance.ball.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount--;
			int count = BallPhysicsManager.Instance.ball.GetComponent<VelocityTriggerCounter> ().ballOneTriggeredCount;
			if (count < 0) {
				BallPhysicsManager.Instance.ball.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount = 0;
			}
			VelocityResponse.Instance.removeVelocityVector (gameObject);
			Debug.Log ("BallPhysicsManager update  VelocityVector and ball one collision removed");
			BallPhysicsManager.Instance.updateVelocity ();
		}
		else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name){
			BallPhysicsManager.Instance.ballTwo.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount--;
			int count = BallPhysicsManager.Instance.ballTwo.GetComponent<VelocityTriggerCounter> ().ballOneTriggeredCount;
			if (count < 0) {
				BallPhysicsManager.Instance.ballTwo.GetComponent<VelocityTriggerCounter>().ballOneTriggeredCount = 0;
			}
			VelocityResponseBallTwo.Instance.removeVelocityVector (gameObject);
			Debug.Log ("VelocityVector and ball two collision removed");
			BallPhysicsManager.Instance.updateVelocity ();
		}

    }
    private void OnDestroy()
    {
		VelocityResponse.Instance.removeVelocityVector(gameObject);
    }
}
