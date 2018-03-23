using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindableObject : MonoBehaviour
{
    List<PointInTime> pointsInTime;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }
    public void Rewind()
    {
        int currentPointInTime = RewindManager.Instance.currentPointInTime;
		Debug.Log ("RewindableObject Rewind: currentPointInTime - "+currentPointInTime);
        if (currentPointInTime > 0)
        {
			Debug.Log ("RewindableObject: ApplyPointInTime");
            ApplyPointInTime(currentPointInTime);
        }
    }
    public void EnableRewinding()
    {
        rb.isKinematic = true;
    }
    public void ResetRewind()
    {
		Debug.Log ("This gets called");
        rb.isKinematic = false;
        ApplyPointInTime(0);
		RewindManager.Instance.currentRewindables = new List<GameObject>();
    }
    public void Record()
    {
		Vector3 scale,velocity,force,position;
		Quaternion rotation;
		scale = getScale ();
		velocity = getVelocity ();
		position = getPosition ();
		rotation = getRotation ();
		force = getForce ();
		Debug.Log ("RewindableObject Record: position: "+position+ " rotation: "+rotation+" scale: "+scale+ " velocity: "+velocity+" force: "+force);
		pointsInTime.Add(new PointInTime(position, rotation, scale, velocity, force));

    }
    Vector3 getPosition()
    {
        return transform.position;
    }
    Quaternion getRotation()
    {
        return transform.rotation;
    }
    Vector3 getScale()
    {
        return transform.localScale;
    }
    Vector3 getVelocity()
    {
        if (gameObject.name.Contains("MainBall"))
        {
			return BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity;
        }
        return new Vector3();
    }
    Vector3 getForce()
    {
        if (gameObject.name.Contains("MainBall"))
        {
            return BallPhysicsManager.Instance.updatedForce;
        }
        return new Vector3();
    }
    void ApplyPointInTime(int pointInTimeIndex)
    {
		Debug.Log ("ApplyPointInTime: "+pointInTimeIndex);
        PointInTime currentPointInTime = pointsInTime[pointInTimeIndex];
        transform.position = currentPointInTime.position;
        transform.rotation = currentPointInTime.rotation;
        transform.localScale = currentPointInTime.scale;
        BallPhysicsManager.Instance.updatedVelocity = currentPointInTime.velocity;
        BallPhysicsManager.Instance.updatedForce = currentPointInTime.force;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
