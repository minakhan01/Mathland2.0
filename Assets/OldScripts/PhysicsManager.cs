using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class PhysicsManager : Singleton<PhysicsManager> {

	public float force = 8f;

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, 0, 0) ; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
