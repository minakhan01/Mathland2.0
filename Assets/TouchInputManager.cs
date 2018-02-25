using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour {

	const float pinchTurnRatio = Mathf.PI / 2;
	const float minTurnAngle = 0;

	const float pinchRatio = 1;
	const float minPinchDistance = 0;

	const float panRatio = 1;
	const float minPanDistance = 0;

	static public float turnAngleDelta;
	static public float turnAngle;

	static public float pinchDistanceDelta;
	static public float pinchDistance;

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
	private float DraggingTreshold=0.7f;
	private float LongPressTreshold=3f;
	float PreviousFingerDistance, zoomDistance, distWeight;

	// Use this for initialization
	void Start () {
		CurrentTouchState = TouchState.Stationary;
	}

	static private float Angle (Vector2 pos1, Vector2 pos2) {
		Vector2 from = pos2 - pos1;
		Vector2 to = new Vector2(1, 0);

		float result = Vector2.Angle( from, to );
		Vector3 cross = Vector3.Cross( from, to );

		if (cross.z > 0) {
			result = 360f - result;
		}

		return result;
	}

	void draggingDetected() {
		CurrentTouchState = TouchState.Repositioning;
		Debug.Log ("Drag Object");
		// ----- UniversalInputManager.updatePosition (-touchDeltaPosition.x * TouchSpeed, -touchDeltaPosition.y * TouchSpeed);
	}

	void rotatingDetected() {
		CurrentTouchState = TouchState.Rotating;
		Debug.Log ("Rotate Object");
		// ------ UniversalInputManager.RotateObject (FingerAngleDelta);
	}

	void pinchingDetected() {
		CurrentTouchState = TouchState.Resizing;
		Debug.Log ("Resize Object");
		// ------ UniversalInputManager.ResizeObject (FingerDistanceDelta);
	}

	void deletingDetected() {
		CurrentTouchState = TouchState.Deleting;
		Debug.Log ("Delete Object");
		// ----- UniversalInputManager.DeleteObject ();
	}

	void stationaryDetected() {
		AccumulateTime = 0;
		CurrentTouchState = TouchState.Stationary;
		Debug.Log ("Stationary Object");
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {

			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				stationaryDetected();
			}
			else if (Input.touchCount == 1) {

				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					AccumulateTime = 0;
				} else if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
					AccumulateTime += Input.GetTouch (0).deltaTime;
					if(AccumulateTime > LongPressTreshold) {
						deletingDetected();
					}
				} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
					if (touchDeltaPosition.magnitude > DraggingTreshold) {
						// Move object across XY plane
						draggingDetected();
					}
				}
				else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					stationaryDetected();
				}
			}
			else if (Input.touchCount == 2) {

				//pinchDistance = pinchDistanceDelta = 0;
				//turnAngle = turnAngleDelta = 0;

				Touch touch1 = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);

				// ... if at least one of them moved ...
				if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) {
					// ... check the delta distance between them ...
					pinchDistance = Vector2.Distance(touch1.position, touch2.position);
					float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
						touch2.position - touch2.deltaPosition);
					pinchDistanceDelta = pinchDistance - prevDistance;

					// ... if it's greater than a minimum threshold, it's a pinch!
					if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance) {
						pinchDistanceDelta *= pinchRatio;
						Debug.Log ("Pinch Distance" + pinchDistanceDelta);
						pinchingDetected();
					} else {
						pinchDistance = pinchDistanceDelta = 0;
					}

					// ... or check the delta angle between them ...
					turnAngle = Angle(touch1.position, touch2.position);
					float prevTurn = Angle(touch1.position - touch1.deltaPosition,
						touch2.position - touch2.deltaPosition);
					turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

					// ... if it's greater than a minimum threshold, it's a turn!
					if (Mathf.Abs(turnAngleDelta) > minTurnAngle) {
						turnAngleDelta *= pinchTurnRatio;
						Debug.Log ("Angle Delta" + turnAngleDelta);
						rotatingDetected();
					} else {
						turnAngle = turnAngleDelta = 0;
					}
				}
			}  
		}
	}
}