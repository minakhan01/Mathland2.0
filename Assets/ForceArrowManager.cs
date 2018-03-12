using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ForceArrowManager : Singleton<ForceArrowManager> {

	//the proportionality contant of (change in arrow size)/(change in force magnitude)
	private const float ARROW_SCALE_PROPORTIONALITY = 0.1f;
	private const float ARROW_CHANGE_DIRECTION_SPEED = 1.0f;
	private float INITIAL_ARROW_HEAD_Z_SCALE;

	//intialize tail and head game objects
	protected GameObject arrowTail;
	protected GameObject arrowHead;
	void Start () {
		arrowTail = gameObject.transform.Find("tailfbd").gameObject;
		arrowHead = arrowTail.transform.Find("headfbd").gameObject;
		INITIAL_ARROW_HEAD_Z_SCALE = arrowHead.transform.localScale.z;
	}


	// Each update, move and rescale arrow to represent proper force specs
	void Update () {
		BallPhysicsManager.Instance.updateVelocityandForce ();
		updateForceArrowAngle ();
		updateForceArrowSize ();
	}
		

	public void updateForceArrowSize()
	{
		Vector3 oldArrowHeadSize = arrowHead.transform.localScale;

		//get new scale of arrow and rescale arrow
		float ballForceMagnitude = BallPhysicsManager.Instance.updatedForce.magnitude;
		float rescaleTail = ballForceMagnitude * ARROW_SCALE_PROPORTIONALITY; 
		float rescaleHead = (1.0f / ballForceMagnitude) * INITIAL_ARROW_HEAD_Z_SCALE; //keeps it same size as parent changes

		//rescale arrow
		arrowTail.transform.localScale = new Vector3 (arrowTail.transform.localScale.x,
				arrowTail.transform.localScale.y, rescaleTail);

		//fix local position of head after tail has been updated
		arrowHead.transform.localScale = new Vector3 (arrowHead.transform.localScale.x,
			arrowHead.transform.localScale.y, rescaleHead);

	}

	public void updateForceArrowAngle()
	{
//		transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, 
//			BallPhysicsManager.Instance.updatedForce);
		Quaternion toRotation = Quaternion.FromToRotation(transform.up, BallPhysicsManager.Instance.updatedForce);
		transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, ARROW_CHANGE_DIRECTION_SPEED * Time.time);
	}
}



