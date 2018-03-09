using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityArrowManager : Singleton<VelocityArrowManager> {

	public GameObject velocityTail, velocityHead;
	private Vector3 initialTailScale, headTailRelativeDistance;

	// Use this for initialization
	void Start () {
		initialTailScale = velocityTail.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		BallPhysicsManager.Instance.updateVelocityandForce ();
		updateVectorArrowAngle ();
		updateVectorArrowPosition ();
		updateVectorArrowSize ();
	}

	public void updateVectorArrowPosition()
	{
		velocityTail.transform.position = BallPhysicsManager.Instance.ball.transform.position;
		Vector3 headpos = velocityHead.transform.position;
		velocityHead.transform.position = new Vector3 (headpos.x, 1.9f*velocityTail.transform.localScale.y, headpos.z);
	}

	public void updateVectorArrowSize()
	{
		float ballVelocityMagnitude = BallPhysicsManager.Instance.updatedVelocity.magnitude;
		velocityTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z*ballVelocityMagnitude);
	}

	public void updateVectorArrowAngle()
	{
		transform.rotation = Quaternion.LookRotation (BallPhysicsManager.Instance.updatedVelocity);
	}
}
