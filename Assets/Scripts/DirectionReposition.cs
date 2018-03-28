using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionReposition : MonoBehaviour {
	private Vector3 initialpos;
	public GameObject sibling;
	float initialsize;
	// Use this for initialization
	void Start () {
		initialpos = sibling.transform.position-transform.position;
		initialsize = sibling.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = sibling.transform.position-initialpos*(sibling.transform.localScale.x/initialsize);
		transform.position = newPos;
		//transform.rotation = sibling.transform.rotation;
	}
	void OnDestroy(){
		Destroy (sibling);
	}
}
