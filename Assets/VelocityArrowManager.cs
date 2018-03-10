using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityArrowManager : Singleton<VelocityArrowManager> {

	public GameObject vectorTail, vectorHead;
	private Vector3 initialTailScale;

	// Use this for initialization
	void Start () {
		initialTailScale = vectorTail.transform.localScale;
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
		vectorTail.transform.position = BallPhysicsManager.Instance.ball.transform.position;
		Vector3 headpos = vectorHead.transform.position;
		vectorHead.transform.position = new Vector3 (headpos.x, 1.9f*vectorTail.transform.localScale.y, headpos.z);
	}

	public void updateVectorArrowSize()
	{
		float ballVelocityMagnitude = BallPhysicsManager.Instance.updatedVelocity.magnitude;
		vectorTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z*ballVelocityMagnitude);
	}

	public void updateVectorArrowAngle(){
		
	Quaternion toRotation = Quaternion.FromToRotation(transform.up, BallPhysicsManager.Instance.updatedVelocity);
	transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1.0f * Time.time);

	}
}
