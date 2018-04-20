using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Initializer : MonoBehaviour {
	public GameObject ball;
	public GameObject ForceField;
	public float ForceMagnitude = 1.0f;

	// Use this for initialization
	void Start () {
		ForceMagnitude *= 0.001f;
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, ForceMagnitude, DirectionArrow.transform.localScale.z);
		RewindManager.Instance.maxRecordTimeInit = 2;
		ball.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.5f;
		ball.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
		//StrobingHandler.Instance.countInterval = 13;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
