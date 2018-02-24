using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObjectRawInput : MonoBehaviour {
	public float initialFingerDistance;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			Vector3 initialPosition = rb.position;
			if (Input.touchCount == 2) {
			     // initial distance between finger
			     Touch touchDistance = Input.GetTouch(1);
			     if (touch.phase == TouchPhase.Began) {
				         initialFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
			     } else {
				         float currentFingerDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
				         float factor = currentFingerDistance / initialFingerDistance;
//				         GameStateFunctions.resizeObject (factor);
			     }
		  }
    }
  }
}
