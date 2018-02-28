using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class TouchListenerManager : Singleton<GameStateManager> {

	// Use this for initialization
	void Start () {
		
	}

	private bool longPressObjectDetected()
	{
		if (TouchInputManager.selectedObject == null)
			return false;
		else if (TouchInputManager.CurrentTouchState == TouchInputManager.TouchState.longPress)
			return true;
		else
			return false;
	}

	private bool objectDragging()
	{
		return (TouchInputManager.CurrentTouchState == TouchInputManager.TouchState.Repositioning && TouchInputManager.selectedObject != null);
	}

	private bool longPressOutsideDetected()
	{
		if (TouchInputManager.selectedObject == null && TouchInputManager.CurrentTouchState == TouchInputManager.TouchState.longPress)
			return true;
		else
			return false;
	}
	
	// Update is called once per frame
	void Update () {

		// for testing
		if (objectDragging())
		{
			Debug.Log ("dragging detected");
			TouchInputManager.selectedObject.transform.position = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 1);
		}


		if (GameStateManager.currentDisplayState == GameStateManager.GameDisplayState.PLAY_SCREEN && longPressObjectDetected ()) 
		{
			Debug.Log ("change to modify screen");
			GameStateManager.switchDisplayState ();
		} 
		else if (GameStateManager.currentDisplayState == GameStateManager.GameDisplayState.MODIFY_SCREEN && objectDragging())
		{
			TouchInputManager.selectedObject.transform.position = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 1);
		}
		else if (GameStateManager.currentDisplayState == GameStateManager.GameDisplayState.MODIFY_SCREEN && longPressOutsideDetected ()) 
		{
			Debug.Log ("change to play screen");
			GameStateManager.switchDisplayState ();
		}


//		if (Input.touchCount == 1) {
//			Touch touch = Input.GetTouch(0);
//			Vector3 initialPosition = rb.position;
//			bool drag = false;
//			// if user touch the object = drag
//			if (touch.phase == TouchPhase.Began) {
//				initialPosition = rb.position;
//				// if the touch is on the object = that means user want to drag the object
//				if (Vector2.Distance (Input.touches[0].position, new Vector2(initialPosition.x, initialPosition.y)) < 0.05) {
//					drag = true;
//				}
//			} else if (drag == true) {
//				Vector2 currentPosition = Input.touches[0].position;
//				//				GameStateFunctions.moveObject(currentPosition);
//			}
//		}
	}


}
