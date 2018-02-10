using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameStateManager {

	//gameState is a string that can change between "build", "play", "tool menu", "main menu",
	// "graph menu", and "select object"
	public static string gameState = "build"; //initialized to build for debugging purposes
	public static GameObject selectedObject;



	//checks if a valid object was clicked --> should only do this for gameobjects, not buttons!
	public static void SelectObject(Ray clickPosition) {
		RaycastHit hit;
		if (Physics.Raycast (clickPosition, out hit)) {
			if (hit.collider != null) {
				GameObject collision = hit.transform.gameObject; //get gameobject clicked on
				if (collision.tag.ToString () == gameState) { //check if proper gamestate
					gameState = "select object";
					selectedObject = collision;
					//run scripts here or further differentiate types for buttons??
				}
			}
		}
	}


	//checks if valid button is pressed
	public static void ButtonPressed(Button button) {
		//to do: figure out how buttons "on click" function works 
		//also figure out best way to send button data around and call
		//button functions
	}


	//checks if valid object needs to be rotated
	public static void rotateObject(Vector3 angles) {
		if (gameState == "select object") {
			selectedObject.transform.Rotate (angles);
		}
	}


	//checks if valid object needs to be moved 
	public static void moveObject(Vector3 newPosition) {
		if (gameState == "select object") {
			selectedObject.transform.Translate (newPosition * Time.deltaTime);
		}
	}


	//checks if valid object needs to be rescaled
	public static void resizeObject(float percent_decimal) {
		if (gameState == "select object") {
			//get size of object
			Vector3 object_size = selectedObject.GetComponent<Collider> ().bounds.size;
			//scale object accordingly
			selectedObject.transform.localScale += (object_size * percent_decimal);
		}
	}
		

}
