using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindableObject : MonoBehaviour {
	List<PointInTime> pointsInTime;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		PointInTime=new List<PointInTime> ();
		rb = GetComponent<Rigidbody> ();
	}
	void Rewind(){
		int currentPointInTime = RewindManager.Instance.currentPointInTime;
		if(currentPointInTime>0){
			PointInTime pointInTime=pointsInTime[currentPointInTime];
			ApplyPointInTime (pointInTime);
		}
	}
	void EnableRewinding(){
		rb.isKinematic = true;
	}
	void ResetRewind(){
		rb.isKinematic = false;
		ApplyPointInTime (0);
		RewindManager.Instance.currentRewindables = new List<RewindableObject> ();
	}
	void Record(){
		pointsInTime.Add (getPosition (), getRotation (), getScale (), getVelocity (), getForce ());

	}
	Vector3 getPosition(){
		return transform.position;
	}
	Quaternion getRotation(){
		return transform.rotation;
	}
	Vector3 getScale(){
		return transform.localScale;
	}
	Vector3 getVelocity(){
		if(gameObject.name.Contains("MainBall")){
			return BallPhysicsManager.Instance.updatedVelocity;
	}
	}
	Vector3 getForce(){
		if(gameObject.name.Contains("MainBall")){
			return BallPhysicsManager.Instance.updatedForce;
		}
	}
	void ApplyPointInTime(int pointInTimeIndex){
		PointInTime currentPointInTime = pointsInTime [pointInTimeIndex];
		transform.position = currentPointInTime.position;
		transform.rotation = currentPointInTime.rotation;
		transform.localScale = currentPointInTime.scale;
		BallPhysicsManager.Instance.updatedVelocity = currentPointInTime.velocity;
		BallPhysicsManager.Instance.updatedForce = currentPointInTime.force;
	}
	// Update is called once per frame
	void Update () {

	}
}
