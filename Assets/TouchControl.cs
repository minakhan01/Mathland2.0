using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour {
	public float initialFingerDistance, objectMass;
	public Rigidbody rb;
	public static Transform ScaleTransform;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.mass = objectMass;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			//if hold for several seconds, go to UniversalInteraction select

		}
		if (Input.touchCount == 2) {
			// initial distance between finger
			Touch touch = Input.GetTouch(1);
			if (touch.phase == TouchPhase.Began) {
				initialFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
			} else {
				float currentFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
				float factor = currentFingerDistance / initialFingerDistance;
				UniversalInteractions.resizeObject (factor);
				// change mass of object
				rb.mass = objectMass * factor;
			}
		}
	}
}
