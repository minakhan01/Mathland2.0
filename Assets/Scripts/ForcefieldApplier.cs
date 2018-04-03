using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForcefieldApplier : MonoBehaviour {
	public GameObject siblingarrow;
	int collisionCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider collidee)

	{
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name ||
			collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			collisionCount++;
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.blue;
		}
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name && collisionCount == 1) {
			ForceResponse.Instance.addForceVector (siblingarrow);
		} else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name && collisionCount == 1) {
			ForceResponseBallTwo.Instance.addForceVector (siblingarrow);
		}
			//AffectedObjects.Add(collidee.gameObject);
			Debug.Log("Thing entered ForceField");
	}
	void OnTriggerExit(Collider collidee)
	{
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name ||
			collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			collisionCount--;
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.red;
			if (collisionCount < 0) {
				collisionCount = 0;
			}
		}
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			ForceResponse.Instance.removeForceVector (siblingarrow);
		} else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			ForceResponseBallTwo.Instance.removeForceVector (siblingarrow);
		}
			Debug.Log("Thing escaped ForceField");
	}
	void OnDestroy(){
		Destroy (siblingarrow); 
	}
}
