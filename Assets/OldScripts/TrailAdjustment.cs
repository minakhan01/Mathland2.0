using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailAdjustment : MonoBehaviour {

    private TrailRenderer tr;
    private Rigidbody rigidbody;
    private float initialVelocity;
    private bool velocitySet = false;

    // Use this for initialization
    void Start () {
        tr = GetComponent<TrailRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float velocity = rigidbody.velocity.magnitude;

        if (!velocitySet && velocity > 5) {
            initialVelocity = velocity;
            Debug.Log("initial velocity: " + initialVelocity);
            velocitySet = true;
        }

        AnimationCurve curve = new AnimationCurve();
        {
            curve.AddKey(0.0f, 0.0f);
            curve.AddKey(1.0f, 1.0f);
        }
        tr.widthCurve = curve;
        tr.widthMultiplier = velocity / initialVelocity * 0.05f;
        Debug.Log("velocity: " + rigidbody.velocity.magnitude);
    }
}
