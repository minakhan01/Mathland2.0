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

	public enum TouchState {Stationary, Repositioning, Resizing, Rotating, longPress};
	public static TouchState CurrentTouchState;
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

	public static GameObject selectedObject;

	// Use this for initialization
	void Start () {
		CurrentTouchState = TouchState.Stationary;
		selectedObject = null;
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

	void longPressDetected() {
		CurrentTouchState = TouchState.longPress;
		Debug.Log ("Long Press Object");
		// ----- UniversalInputManager.DeleteObject ();
	}

	void stationaryDetected() {
		AccumulateTime = 0;
		CurrentTouchState = TouchState.Stationary;
		Debug.Log ("Stationary Object");
	}

	private Ray touchPositionRay;

	void doRayCasting(Touch touch)
	{
		//get 2D pixel position of touch, convert into 3D point in space, turn into ray from cameraq
		Camera camera = Camera.current;
		Vector2 touchPosition = touch.position; //2D touch position in pixels

		/////CONVERT PIXELS POSITION INTO 3D RAY
		touchPositionRay = camera.ScreenPointToRay (touchPosition);
	}

	void checkRayCastHit()
	{
		RaycastHit hit;
		if (Physics.Raycast (touchPositionRay, out hit)) {
			if (hit.collider != null) {
				GameObject collision = hit.transform.gameObject; //get gameobject clicked on
				selectedObject = collision;

				Debug.Log ("selectedObject: "+selectedObject);
				//for fix: look into GetComponent
				//only objects with modifiable should be selected
				//selectedObject.GetComponent<UniversalInteractions> ().SelectObject ();
			}
		} else {
			selectedObject = null;
			Debug.Log (selectedObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Ended) {
				stationaryDetected();
			}
			else if (Input.touchCount == 1) {
				if (touch.phase == TouchPhase.Began) {
					AccumulateTime = 0;
					doRayCasting (touch);
					checkRayCastHit ();
				} else if (touch.phase == TouchPhase.Stationary) {
					AccumulateTime += Input.GetTouch (0).deltaTime;
					if(AccumulateTime > LongPressTreshold) {
						longPressDetected();
					}
				} else if (touch.phase == TouchPhase.Moved) {
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
					if (touchDeltaPosition.magnitude > DraggingTreshold) {
						// Move object across XY plane
						doRayCasting (touch);
						checkRayCastHit ();
						draggingDetected();
					}
				}
				else if (touch.phase == TouchPhase.Ended) {
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