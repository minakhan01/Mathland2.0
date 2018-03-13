using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityArrowManager : Singleton<VelocityArrowManager> {

	public GameObject velocityTail, velocityHead;
	private Vector3 initialTailScale, initialHeadScale;

	// Use this for initialization
	void Start () {
		initialTailScale = velocityTail.transform.localScale;
		initialHeadScale = velocityHead.transform.localScale;
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
		//Vector3 vectorTailSize = velocityTail.GetComponent<Renderer>().bounds.size; 
		//velocityHead.transform.position = transform.position + (Vector3.Scale(velocityTail.transform.forward.normalized, vectorTailSize));
	}

	public void updateVelocityArrowSize()
	{
		float ballVelocityMagnitude = BallPhysicsManager.Instance.updatedVelocity.magnitude;
		velocityTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z*ballVelocityMagnitude);
		velocityHead.transform.localScale = new Vector3 (initialHeadScale.x, initialHeadScale.y, initialHeadScale.z*(1/ballVelocityMagnitude));
	}


	public void updateVelocityArrowAngle()
	{
		//Quaternion toRotation = Quaternion.FromToRotation(transform.up, BallPhysicsManager.Instance.updatedVelocity);
		//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(BallPhysicsManager.Instance.updatedVelocity), 1.0f * Time.time);
		//transform.rotation = Quaternion.LookRotation (new Vector3(BallPhysicsManager.Instance.updatedVelocity.z, BallPhysicsManager.Instance.updatedVelocity.y, BallPhysicsManager.Instance.updatedVelocity.x));
		Vector3 direction=BallPhysicsManager.Instance.updatedVelocity;
		Quaternion rot = Quaternion.LookRotation (direction)*Quaternion.Euler(90, 0, 0);
		transform.rotation = rot;
	}
}
