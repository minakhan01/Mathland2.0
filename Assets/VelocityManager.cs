﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityManager : MonoBehaviour {
    public Vector3 VelocityVector=new Vector3(0,0,0);
    public float VelocityVectorMagnitude=1.0f;
    public GameObject BALL;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collidee)
    {
        if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
        {
            BALL = collidee.gameObject;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            //GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Active");
            collidee.gameObject.GetComponent<VelocityReactor>().addVelocityVector(gameObject);
        }
    }
    private void OnTriggerExit(Collider collidee)
    {
        if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
        {
            //BALL = null;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            collidee.gameObject.GetComponent<VelocityReactor>().removeVelocityVector(gameObject);
            //GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Inactive");
        }
    }
    private void OnDestroy()
    {
        BALL.GetComponent<VelocityReactor>().removeVelocityVector(gameObject);
    }
}