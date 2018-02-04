using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchScale : MonoBehaviour {
	public float initialFingerDistance, objectMass;
	public Vector3 initialSize;
	public Rigidbody rb;
	public static Transform ScaleTransform;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		rb.mass = objectMass;
	}

	void Update () {
		if (Input.touchCount == 2) {
			// initial distance between finger
			Touch touch = Input.GetTouch(1);
			if (touch.phase == TouchPhase.Began) {
				initialFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
				initialSize = ScaleTransform.localScale;
			} else {
				float currentFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
				float factor = currentFingerDistance / initialFingerDistance;
				ScaleTransform.localScale = initialSize * factor;
				// change mass of object
				rb.mass = objectMass * factor;
			}
		}
	}
}
