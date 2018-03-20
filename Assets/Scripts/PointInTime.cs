
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointInTime : MonoBehaviour {

	public Quaternion rotation;
	public Vector3 scale,velocity,force,position;
	// Use this for initialization


	public PointInTime(Vector3 _position,Quaternion _rotation,Vector3 _scale,Vector3 _velocity,Vector3 _force){
		position = _position;
		rotation = _rotation;
		scale = _scale;
		velocity = _velocity;
		force = _force;

	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
