using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene10Initializer : MonoBehaviour {

	public GameObject VelocityArrow;
	public float VelocityMagnitude = 5.0f;

	// Use this for initialization
	void Start () {
		VelocityMagnitude *= 0.001f;
		VelocityArrow.transform.localScale = new Vector3 (VelocityMagnitude, VelocityMagnitude, VelocityMagnitude);
	}

	// Update is called once per frame
	void Update () {

	}
}
