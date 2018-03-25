using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Initializer : MonoBehaviour {

	public GameObject VelocityArrow2;

	public GameObject ForceField;
	public float ForceMagnitude = 1f;

	public float Velocity2Magnitude = 0.6f;

	// Use this for initialization
	void Start () {
		ForceMagnitude *= 0.001f;
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
//		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, ForceMagnitude, DirectionArrow.transform.localScale.z);

		Velocity2Magnitude *= 0.002f;
		VelocityArrow2.transform.localScale = new Vector3 (Velocity2Magnitude, Velocity2Magnitude, Velocity2Magnitude);
	}

	// Update is called once per frame
	void Update () {

	}
}
