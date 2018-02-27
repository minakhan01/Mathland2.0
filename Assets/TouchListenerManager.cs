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

	private bool longPressOutsideDetected()
	{
		if (TouchInputManager.selectedObject == null && TouchInputManager.CurrentTouchState == TouchInputManager.TouchState.longPress)
			return true;
		else
			return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.currentDisplayState == GameStateManager.GameDisplayState.PLAY_SCREEN && longPressObjectDetected ()) 
		{
			Debug.Log ("change to modify screen");
			GameStateManager.switchDisplayState ();
		} 
		else if (GameStateManager.currentDisplayState == GameStateManager.GameDisplayState.MODIFY_SCREEN && longPressOutsideDetected ()) 
		{
			Debug.Log ("change to play screen");
			GameStateManager.switchDisplayState ();
		}
	}
}
