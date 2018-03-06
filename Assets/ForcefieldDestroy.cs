using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForcefieldDestroy : MonoBehaviour {
	public GameObject siblingarrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy(){
		Destroy (siblingarrow); 
	}
}
