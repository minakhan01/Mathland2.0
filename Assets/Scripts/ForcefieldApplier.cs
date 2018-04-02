using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForcefieldApplier : MonoBehaviour {
	public GameObject siblingarrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider collidee)

	{
		
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount++;
			int count = BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount;
			if (count == 1) {
				ForceResponse.Instance.addForceVector (siblingarrow);
				BallPhysicsManager.Instance.updateForce();
			}
		} else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount++;
			int count = BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount;
			if (count == 1) {
				ForceResponseBallTwo.Instance.addForceVector (siblingarrow);
				BallPhysicsManager.Instance.updateForce();
			}
		}

			//AffectedObjects.Add(collidee.gameObject);
			Debug.Log("Thing entered ForceField");
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
	void OnTriggerExit(Collider collidee)
	{
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount--;
			int count = BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter> ().ballOneTriggeredCount;
			if (count < 0) {
				BallPhysicsManager.Instance.ball.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount = 0;
			}
			ForceResponse.Instance.removeForceVector (siblingarrow);
			BallPhysicsManager.Instance.updateForce();
		} else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
			BallPhysicsManager.Instance.ballTwo.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount--;
			int count = BallPhysicsManager.Instance.ballTwo.GetComponent<ForceTriggerCounter> ().ballOneTriggeredCount;
			if (count < 0) {
				BallPhysicsManager.Instance.ballTwo.GetComponent<ForceTriggerCounter>().ballOneTriggeredCount = 0;
			}
			ForceResponseBallTwo.Instance.removeForceVector (siblingarrow);
			BallPhysicsManager.Instance.updateForce();
		}


			Debug.Log("Thing escaped ForceField");
			siblingarrow.GetComponent<MeshRenderer> ().material.color = Color.red;
	}
	void OnDestroy(){
		Destroy (siblingarrow); 
	}
}
