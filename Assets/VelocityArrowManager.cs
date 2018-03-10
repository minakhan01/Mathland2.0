using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityArrowManager : Singleton<VelocityArrowManager> {

	public GameObject velocityTail, velocityHead;
	private Vector3 initialTailScale;

	// Use this for initialization
	void Start () {
		initialTailScale = velocityTail.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		BallPhysicsManager.Instance.updateVelocityandForce ();
		updateVelocityArrowAngle ();
		updateVelocityArrowSize ();
		updateVelocityArrowPosition ();
	}

	public void updateVelocityArrowPosition()
	{
		velocityTail.transform.position = BallPhysicsManager.Instance.ball.transform.position;
		Vector3 vectorTailSize = velocityTail.GetComponent<Renderer>().bounds.size; 
		velocityHead.transform.position = transform.position + (Vector3.Scale(velocityTail.transform.forward.normalized, vectorTailSize));
	}

	public void updateVelocityArrowSize()
	{
		float ballVelocityMagnitude = BallPhysicsManager.Instance.updatedVelocity.magnitude;
		velocityTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z*ballVelocityMagnitude);
	}


	public void updateVelocityArrowAngle()
	{
		transform.rotation = Quaternion.LookRotation (BallPhysicsManager.Instance.updatedVelocity);
	}
}
