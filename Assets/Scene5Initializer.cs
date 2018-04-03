using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene5Initializer : MonoBehaviour {
	public GameObject ball;
	public GameObject VelocityArrow1, VelocityArrow2;
	public float Velocity1Magnitude = 3.0f;
	public float Velocity2Magnitude = 4.0f;

	// Use this for initialization
	void Start () {
		Velocity1Magnitude *= 0.002f;
		Velocity2Magnitude *= 0.002f;
		VelocityArrow1.transform.localScale = new Vector3 (Velocity1Magnitude, Velocity1Magnitude, Velocity1Magnitude);
		VelocityArrow2.transform.localScale = new Vector3 (Velocity2Magnitude, Velocity2Magnitude, Velocity2Magnitude);
		RewindManager.Instance.maxRecordTimeInit = 5;
		ball.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
