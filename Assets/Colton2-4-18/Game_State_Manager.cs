using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_State_Manager {

	//game states that can exist are:
	//"build" "play" "tool menu" "graph menu" "start menu"
	private static string game_state = "build"; //will implement start menu later

	public static string Game_state {
		
		get {
			return game_state;
		}
		set {
			game_state = value;
		}

	}
		
		
}
