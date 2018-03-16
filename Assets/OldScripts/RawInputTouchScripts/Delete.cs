using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {
	
	public float tapSpeed = 0.5f; //in seconds

	private float lastTapTime = 0;
	// Use this for initialization
	void Start () {
		lastTapTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount == 1){

			if((Time.time - lastTapTime) < tapSpeed){
				//Ideally a button should appear here with a confirmation for removing the object
				Destroy (gameObject);

			}
			lastTapTime = Time.time;

		}

	}

}
