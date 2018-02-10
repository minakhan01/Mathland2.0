using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	public bool screenSwipe;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch (0);

			//if we are in build mode and it is a swipe down, open the tools menu
			if (Game_State_Manager.Game_state == "build" || Game_State_Manager.Game_state == "tools menu") 
			{
					
				switch (touch.phase) 
				{
					//check if screen touched
					case TouchPhase.Began:
						startPos = touch.position;
						break;
					
					//check if screen was swiped downward
					case TouchPhase.Moved:
						direction = touch.position - startPos;
						//check if swipe down and in regular menu
						if (-direction [1] > Screen.width * .4 && startPos [1] > Screen.width * .9) {
							OpenToolsMenu ();
						} 
						else if (direction [1] > Screen.width * .4 && startPos [1] < Screen.width * .1) {
							CloseToolsMenu ();
						}

						break;
				}
			}
		}
	}
		

	//Commands for opening and closing tools menu, can add animation later
	public GameObject toolsMenu;
	void OpenToolsMenu ()
	{
		toolsMenu.SetActive (true);
		Game_State_Manager.Game_state = "tools menu";
	}

	void CloseToolsMenu ()
	{
		toolsMenu.SetActive (false);
		Game_State_Manager.Game_state = "build";
	}

}
