using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour {
    public bool isBackwards = false; 
	// Use this for initialization
	void Start () {
        transform.LookAt(Camera.main.transform);

    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        if (!isBackwards) transform.Rotate(new Vector3(0, 180, 0));
    }
}
