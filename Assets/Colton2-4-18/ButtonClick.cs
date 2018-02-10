using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

	public GameObject playText;
	public GameObject buildText;

	// Use this for initialization
	void Start () {
		Button btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}


	void TaskOnClick () {
		//if play button is clicked
		if (Game_State_Manager.Game_state == "play") {
			Game_State_Manager.Game_state = "play";
			playText.SetActive (false);
			buildText.SetActive (true);
		}
		//if build button is clicked
		else {
			Game_State_Manager.Game_state = "build";
			playText.SetActive (true);
			buildText.SetActive (false);
		}
	}

}


