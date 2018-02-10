using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalInteractions : MonoBehaviour {

	/**
	 * Device input will call these scripts and send in the input.
	 * This will in turn call scripts in GameStateManager which will
	 * process the interaction input
	**/ 


	//to be called if object select command occurs
	public static void selectObject (Ray clickPosition) {
		GameStateManager.SelectObject (clickPosition);
	}

	//to be called if button is pressed
	public static void pressButton (Button button) {
	}

	//to be called if rotation command occurs
	public static void rotateObject (Vector3 angles) {
		GameStateManager.rotateObject (angles);
	}

	//to be called if move command occurs
	public static void moveObject (Vector3 newPosition) {
		GameStateManager.moveObject (newPosition);
	}

	//to be called if resize command occurs
	public static void resizeObject (float percent_scaled) {
		GameStateManager.resizeObject (percent_scaled);
	}
		
}
