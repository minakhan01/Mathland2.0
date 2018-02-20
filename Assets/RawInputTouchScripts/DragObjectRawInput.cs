using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectRawInput : MonoBehaviour {
	public Rigidbody rb;
	public static Transform ScaleTransform;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			Vector3 initialPosition = rb.position;
			bool drag = false;
			// if user touch the object = drag
			if (touch.phase == TouchPhase.Began) {
				initialPosition = rb.position;
				// if the touch is on the object = that means user want to drag the object
				if (Vector2.Distance (Input.touches[0].position, new Vector2(initialPosition.x, initialPosition.y)) < 0.05) {
					drag = true;
				}
			} else if (drag == true) {
				Vector2 currentPosition = Input.touches[0].position;
				GameStateFunctions.moveObject(currentPosition);
			}
		}
	}
}
