using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene10Initializer : MonoBehaviour {
	public GameObject ball;
	public GameObject VelocityArrow;
	public float VelocityMagnitude = 5.0f;

	// Use this for initialization
	void Start () {
		VelocityMagnitude *= 0.001f;
		VelocityArrow.transform.localScale = new Vector3 (VelocityMagnitude, VelocityMagnitude, VelocityMagnitude);
		RewindManager.Instance.maxRecordTimeInit = 35;
		ball.GetComponentInChildren<StrobingHandler> ().VelocityConst = 0.36f;
		ball.GetComponentInChildren<StrobingHandler> ().ForceConst = 0.35f;
		GameStateManager.Instance.sceneHasRope = true;
		BallPhysicsManager.Instance.isScene10 = true;
		BallPhysicsManager.Instance.startStrobe = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
