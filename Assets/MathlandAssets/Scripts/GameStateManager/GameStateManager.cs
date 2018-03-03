using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity;


/// <summary>
/// Tracks the state of game as player builds, plays,
/// edits objects, and more.
/// </summary>
public class GameStateManager : Singleton<GameStateManager> {

	//game states are represented as an enum
	public enum gameState {PLAY, BUILD};
	public enum GameDisplayState { PLAY_SCREEN, MODIFY_SCREEN };
	public static gameState currentGameState { get; set; }
	public static GameDisplayState currentDisplayState;

	public static void switchDisplayState ()
	{
		Debug.Log ("current game state " + currentGameState);
		if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
			GameStateManager.currentGameState = GameStateManager.gameState.PLAY;
			UIManager.Instance.switchUI ();
		}
		else if (GameStateManager.currentGameState == GameStateManager.gameState.PLAY) {
			GameStateManager.currentGameState = GameStateManager.gameState.BUILD;
			UIManager.Instance.switchUI ();
		}
		Debug.Log ("Switch Display State");
	}

	void Start() {
		//initialize starting gameState to BUILD for now
	}
		

}
