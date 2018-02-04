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
		rb = balls.GetComponent<Rigidbody>();
		if (Game_State_Manager.Game_state) {
			rb.useGravity = true;
		}
		else {
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
		}

	}
}
