using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Initializer : MonoBehaviour {
	public GameObject ball1, ball2;
	public GameObject VelocityArrow2;

	public GameObject ForceField;
	public float ForceMagnitude = 1f;

	public float Velocity2Magnitude = 0.5f;

	// Use this for initialization
	void Start () {
		ForceMagnitude *= 0.001f;
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
//		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, ForceMagnitude, DirectionArrow.transform.localScale.z);

		Velocity2Magnitude *= 0.002f;
		VelocityArrow2.transform.localScale = new Vector3 (Velocity2Magnitude, Velocity2Magnitude, Velocity2Magnitude);

		RewindManager.Instance.maxRecordTimeInit = 1;

		ball1.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball1.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.15f;
		ball2.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball2.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.15f;
	}

	// Update is called once per frame
	void Update () {

	}
}
