using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_State_Manager {

	private static bool game_state = false;

	public static bool Game_state {
		
		get {
			return game_state;
		}
		set {
			game_state = value;
		}

	}
		
		
}
