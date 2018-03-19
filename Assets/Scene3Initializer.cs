using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Initializer : MonoBehaviour {

	public GameObject ForceField;

	// Use this for initialization
	void Start () {
		GameObject DirectionArrow;
		DirectionArrow = ForceField.transform.Find ("Direction").gameObject;
		DirectionArrow.transform.localScale = new Vector3(DirectionArrow.transform.localScale.x, 0.001f, DirectionArrow.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
