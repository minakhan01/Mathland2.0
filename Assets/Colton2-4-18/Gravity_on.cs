using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_on : MonoBehaviour {
	
	public GameObject balls;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = balls.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Game_State_Manager.Game_state == "play") {
			rb.useGravity = true;
		}
		else {
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
		}

	}
}
