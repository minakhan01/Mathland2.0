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
			Touch touch = Input.GetTouch(0);
			Vector3 initialPosition = rb.position;
			bool drag = false;
			// if user touch the object = drag
			if (touch.phase == TouchPhase.Began) {
				initialPosition = rb.position;
				if (Vector2.Distance (Input.touches[0].position, new Vector2(initialPosition.x, initialPosition.y)) < 0.0005) {
					drag = true;
				}
			} else if (drag == true) {
				Vector2 currentPosition = Input.touches[0].position;
				UniversalInteractions.moveObject(currentPosition);
			}
		}
		else if (Input.touchCount == 2) {
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
