using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Initializer : MonoBehaviour {
	public GameObject ball;
	public GameObject ForceField, VelocityArrow;
	public float ForceMagnitude = 1.0f;
	public float VelocityMagnitude = 5.0f;

	// Use this for initialization
	void Start () {
		ForceMagnitude *= 0.002f;
		VelocityMagnitude *= 0.001f;
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, ForceMagnitude, DirectionArrow.transform.localScale.z);
		VelocityArrow.transform.localScale = new Vector3 (VelocityMagnitude, VelocityMagnitude, VelocityMagnitude);
		RewindManager.Instance.maxRecordTimeInit = 2;
		ball.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.35f;
		ball.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
