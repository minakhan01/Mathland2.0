using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ForceArrowManager : Singleton<ForceArrowManager> {

	//the proportionality contant of (change in arrow size)/(change in force magnitude)
	private const float ARROW_SCALE_PROPORTIONALITY = 0.1f;
	private const float INITIAL_ARROW_HEAD_Z_SCALE = 08f;

	//start by getting tail and head children of arrow
	protected GameObject arrowTail;
	protected GameObject arrowHead;

	// initialize tail scale and relative distance
	void Start () {
		arrowTail = gameObject.transform.Find("tailfbd").gameObject;
		arrowHead = arrowTail.transform.Find("headfbd").gameObject;
	}


	// Each update, move and rescale arrow to represent proper force specs
	void Update () {
		BallPhysicsManager.Instance.updateVelocityandForce ();
		updateForceArrowAngle ();
		updateForceArrowPosition ();
		updateForceArrowSize ();

		Debug.Log ("position of arrow is" + gameObject.transform.position);
	}

	//update tail and head posititions with respect to ball
	public void updateForceArrowPosition()
	{
		//when the ball moves, all its children should move too
	}

	public void updateForceArrowSize()
	{
		Vector3 oldArrowHeadSize = arrowHead.transform.localScale;

		//get new scale of arrow and rescale arrow
		float ballForceMagnitude = BallPhysicsManager.Instance.updatedForce.magnitude;
		float rescaleTail = ballForceMagnitude * ARROW_SCALE_PROPORTIONALITY; 
		float rescaleHead = (1.0f / ballForceMagnitude) * INITIAL_ARROW_HEAD_Z_SCALE;

		//rescale arrow
		arrowTail.transform.localScale = new Vector3 (arrowTail.transform.localScale.x,
				arrowTail.transform.localScale.y, rescaleTail);

		//fix local position of head after tail has been updated
		arrowHead.transform.localScale = new Vector3 (arrowHead.transform.localScale.x,
			arrowHead.transform.localScale.y, rescaleHead);

	}

	public void updateForceArrowAngle()
	{
//		Vector3 forceDirectionAngle = Vector3.Angle(Vector3.up, BallPhysicsManager.Instance.updatedForce);
//		gameObject.transform.localEulerAngles = forceDirectionAngle;
		transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, 
			BallPhysicsManager.Instance.updatedForce);
		Debug.Log ("direction of local arrow is" + transform.rotation.eulerAngles);
	}
}



