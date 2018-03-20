﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Initializer : MonoBehaviour {

	public GameObject ForceField;
	public float ForceMagnitude = 1.0f;

	// Use this for initialization
	void Start () {
		ForceMagnitude *= 0.001f;
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, ForceMagnitude, DirectionArrow.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}