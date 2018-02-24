using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity;

public class UniversalInteractionsManager : Singleton<UniversalInteractionsManager> {

	/// <summary>
	/// Device input will call these scripts and send in the input. 
	/// This will check with game manager and, if the game state is
	/// correct, will execute the input.
	/// </summary>


	//to be called if button is pressed
	public void pressButton (Button button) {
		if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
			throw new RuntimeException ("Not implemented yet");
		}
	}

	//to be called if rotation command occurs
	public void rotateObject (Vector3 angles) {
		if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
			throw new RuntimeException ("Not implemented yet");
		}
	}

	//to be called if move command occurs
	public void moveObject (Vector3 newPosition) {
		if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
			throw new RuntimeException ("Not implemented yet");
		}
	}

	//to be called if resize command occurs
	public void resizeObject (float percent_scaled) {
		if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
			throw new RuntimeException ("Not implemented yet");
		}
	}
		
}
