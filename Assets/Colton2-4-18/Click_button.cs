using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_button : MonoBehaviour {

	Material button_material;
	// Use this for initialization
	void Start () {
		//Fetch the Material from the Renderer of the GameObject
		button_material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
			Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit raycastHit;
			if (Physics.Raycast (raycast, out raycastHit)) {
				if (raycastHit.collider.tag == gameObject.tag) {
					//Game_State_Manager.Game_state = !Game_State_Manager.Game_state;
					if (button_material.color != Color.red) {
						button_material.color = Color.red;
					}
					else {
						button_material.color = Color.blue;
					}
				}
			}
		}
	}
}