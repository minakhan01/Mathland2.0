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

    public int scene { get; set; }

	//game states are represented as an enum
	public enum gameState {PLAY, BUILD, REWIND};
	public enum GameDisplayState { PLAY_SCREEN, MODIFY_SCREEN }; // is unused (Mina)
	public enum GamePlayPhysicsState { ON, OFF };
	public static gameState currentGameState { get; set; }
	public static GameDisplayState currentDisplayState;
	public GamePlayPhysicsState currentPhysicsPlayState = GamePlayPhysicsState.OFF;
	public bool experimentModeEnabled = true;
	public bool sceneHasRope = false;


	public static void switchDisplayState ()
	{
		Debug.Log ("current game state " + currentGameState);
		if (!GameStateManager.Instance.experimentModeEnabled) {
			if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD) {
				GameStateManager.currentGameState = GameStateManager.gameState.PLAY;
				//GameStateManager.Instance.currentPhysicsPlayState = GameStateManager.GamePlayPhysicsState.OFF;
				UIManager.Instance.switchScreenUI ();
			} else if (GameStateManager.currentGameState == GameStateManager.gameState.PLAY) {
				GameStateManager.currentGameState = GameStateManager.gameState.BUILD;
				//GameStateManager.Instance.currentPhysicsPlayState = GameStateManager.GamePlayPhysicsState.OFF;
				UIManager.Instance.switchScreenUI ();
			}
		}
		Debug.Log ("Switch Display State");
	}

    public static void switchGameStateMode(gameState gameState) {
        currentGameState = gameState;
        Debug.Log("GAME STATE MANAGER: Current Game State " + currentGameState);
        UIManager.Instance.switchScreenUI();
    }

	public static void resetDisplayState()
	{
		GameStateManager.currentGameState = GameStateManager.gameState.PLAY;
		UIManager.Instance.switchScreenUI ();	
	}

	void Start() {
		//initialize starting gameState to BUILD for now
	}
		

}
