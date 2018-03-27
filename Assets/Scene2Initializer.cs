using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Initializer : MonoBehaviour {
	public GameObject ball1, ball2;
	public GameObject VelocityArrow1, VelocityArrow2;
	public float Velocity1Magnitude = 1.0f;
	public float Velocity2Magnitude = 1.5f;

	// Use this for initialization
	void Start () {
		Velocity1Magnitude *= 0.002f;
		Velocity2Magnitude *= 0.002f;
		VelocityArrow1.transform.localScale = new Vector3 (Velocity1Magnitude, Velocity1Magnitude, Velocity1Magnitude);
		VelocityArrow2.transform.localScale = new Vector3 (Velocity2Magnitude, Velocity2Magnitude, Velocity2Magnitude);
		RewindManager.Instance.maxRecordTimeInit = 1;
		ball1.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball1.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
		ball2.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball2.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
	}

	// Update is called once per frame
	void Update () {

	}
}
