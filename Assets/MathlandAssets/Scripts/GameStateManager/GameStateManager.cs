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
	public enum gameState {BUILD, PLAY, TOOL_MENU, MAIN_MENU, GRAPH_MENU};
	public static gameState currentGameState { get; set; }


	void Start() {
		//initialize starting gameState to BUILD for now
		currentGameState = gameState.BUILD; 
	}
		

}
