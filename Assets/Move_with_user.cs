using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_with_user : MonoBehaviour {

	public GameObject maincamera;
	protected Vector3 maincamera_position;
	public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//make button position always in front of user
		maincamera_position = maincamera.transform.position;
		transform.position = maincamera_position + new Vector3 (.3f, .4f, 1f);

		//make button face camera
		transform.LookAt(target);
	}
}
