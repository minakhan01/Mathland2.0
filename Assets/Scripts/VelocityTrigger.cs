using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTrigger : MonoBehaviour {
    public Vector3 VelocityVector=new Vector3(0,0,0);
    public float VelocityVectorMagnitude=1.0f;
	int collisionCount = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collidee)
    {
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name ||
		    collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			collisionCount++;
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name && collisionCount == 1) {
			VelocityResponse.Instance.addVelocityVector (gameObject);
			Debug.Log ("Mina Debug VelocityTrigger OnTriggerEnter VelocityVector and ball one collided!");
		}
		else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name && collisionCount == 1){
			VelocityResponseBallTwo.Instance.addVelocityVector (gameObject);
			Debug.Log ("Mina Debug VelocityTrigger OnTriggerEnter VelocityVector and ball two collided!");
		}

    }
    private void OnTriggerExit(Collider collidee)
    {
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name ||
			collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			collisionCount--;
			gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
			if (collisionCount < 0) {
				collisionCount = 0;
			}
		}
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			VelocityResponse.Instance.removeVelocityVector (gameObject);
			Debug.Log ("Mina Debug VelocityTrigger OnTriggerExit VelocityVector and ball one collision removed");
		}
		else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name){
			VelocityResponseBallTwo.Instance.removeVelocityVector (gameObject);
			Debug.Log ("Mina Debug VelocityTrigger OnTriggerExit VelocityVector and ball two collision removed");
		}

    }
    private void OnDestroy()
    {
		VelocityResponse.Instance.removeVelocityVector(gameObject);
    }
}
