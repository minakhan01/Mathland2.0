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
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick () {
		Game_State_Manager.Game_state = !Game_State_Manager.Game_state;
		if (Game_State_Manager.Game_state) {
			playText.SetActive (false);
			buildText.SetActive (true);
		}
		else {
			playText.SetActive (true);
			buildText.SetActive (false);
		}
	}

}


