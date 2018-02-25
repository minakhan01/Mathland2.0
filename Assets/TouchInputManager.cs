using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour {

	public enum TouchState {Stationary, Repositioning, Resizing, Rotating, Deleting};
	public TouchState CurrentTouchState;
	private float HoldTime = 5;
	private float AccumulateTime = 0;
	public float zoomNearLimit = 5;
	public float zoomFarLimit = 12;
	public float zoomScreenToWorldRatio = 3.0f;
	public float orbitScreenToWorldRatio = 1.0f;
	public float twistScreenToWorldRatio = 5.0f;
	public float rotateMinLimit = 0;
	public float rotateMaxLimit = 80;
	float PreviousFingerDistance, zoomDistance, distWeight;

	// Use this for initialization
	void Start () {
		CurrentTouchState = TouchState.Stationary;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {

			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				AccumulateTime = 0;
			} else if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
				AccumulateTime += Input.GetTouch (0).deltaTime;
				if(AccumulateTime > 3) {
					CurrentTouchState = TouchState.Deleting;
					Debug.Log ("Delete Object");
					// ----- UniversalInputManager.DeleteObject ();
				}
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				if (touchDeltaPosition.magnitude > 0.5f) {
					// Move object across XY plane
					CurrentTouchState = TouchState.Repositioning;
					Debug.Log ("Drag Object");
					// ----- UniversalInputManager.updatePosition (-touchDeltaPosition.x * TouchSpeed, -touchDeltaPosition.y * TouchSpeed);
				}
			}
			else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				AccumulateTime = 0;
				CurrentTouchState = TouchState.Stationary;
			}
		}
		else if (Input.touchCount == 2) {
			// Codes found at https://answers.unity.com/questions/205342/gesture-rotation-clamp.html
			// with modification

			// fingers data
			Touch Finger_0 = Input.GetTouch(0);
			Touch Finger_1 = Input.GetTouch(1);

			// fingers positions
			Vector3 Finger_0_Position = new Vector3(Finger_0.position.x, Finger_0.position.y, 0);
			Vector3 Finger_1_Position = new Vector3(Finger_1.position.x, Finger_1.position.y, 0);

			// vector of fingers movements
			Vector3 Finger_0_Delta = new Vector3(Finger_0.deltaPosition.x, Finger_0.deltaPosition.y, 0);
			Vector3 Finger_1_Delta = new Vector3(Finger_1.deltaPosition.x, Finger_1.deltaPosition.y, 0);

			// fingers distance
			float FingerDistance = Vector3.Distance(Finger_0.position, Finger_1.position);

			// if both fingers moving
			if (Finger_0.phase == TouchPhase.Moved && Finger_1.phase == TouchPhase.Moved) {

				// fingers moving direction
				Vector3 Finger_0_Direction = Finger_0_Delta.normalized;
				Vector3 Finger_1_Direction = Finger_1_Delta.normalized;

				// dot product of directions
				float dot = Vector3.Dot(Finger_0_Direction, Finger_1_Direction);

				// if fingers moving in opposite directions
				if (dot < -0.7f) {

					float FingerDistanceDelta = FingerDistance - PreviousFingerDistance;

					/* for resizing, the direction of the finger must be opposite of each other, so the
					 sum of the magnitude = 0 */
					if (Mathf.Abs(Finger_0_Direction.magnitude + Finger_1_Direction.magnitude) < 1) {
						CurrentTouchState = TouchState.Resizing;
						Debug.Log ("Resize Object");
						// ------ UniversalInputManager.ResizeObject (FingerDistanceDelta);
					}
					else if (Finger_0_Delta.magnitude > 2 && Finger_1_Delta.magnitude > 2) {
						// detect twist (Rotate object in XY plane)
						CurrentTouchState = TouchState.Rotating;
						Debug.Log ("Rotate Object in XY Plane");

                     	Vector3 fingersDir = (Finger_1_Position - Finger_0_Position).normalized;
                     	Vector3 twistNormal = Vector3.Cross(fingersDir, Vector3.forward);
                     	Vector3 twistAxis = Vector3.Cross(fingersDir, twistNormal);
                     	float averageDelta = (Finger_0_Delta.magnitude + Finger_1_Delta.magnitude) / 2;

						// -------- UniversalInputManager.RotateObjectXY ();
                 	}
					else if(Mathf.Abs(Finger_0_Direction.magnitude - Finger_1_Direction.magnitude) < 1){
						// detect rotate object in yz plane
						/* for rotating, the direction of the finger must be the same, so the
							 difference of the magnitude = 0 */
						CurrentTouchState = TouchState.Rotating;
						Debug.Log("Rotate Object in YZ Plane");
						// --------- UniversalInputManager.RotateObjectYZ (FingerDistanceDelta);
					}
				}
			}

			// record last distance, for delta distances
			PreviousFingerDistance = FingerDistance;

			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				CurrentTouchState = TouchState.Stationary;
			}
		}

	}
}
