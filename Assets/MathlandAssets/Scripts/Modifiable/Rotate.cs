using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void rotate (float valueSlider) {
        Vector3 newValue = transform.localEulerAngles + new Vector3(valueSlider, valueSlider, valueSlider);
        transform.Rotate(newValue);
    }

    public void rotateX(float valueSlider)
    {
        Vector3 newValue = transform.localEulerAngles + new Vector3(valueSlider, 0, 0);
        transform.Rotate(newValue);
    }

    public void rotateY(float valueSlider)
    {
        Vector3 newValue = transform.localEulerAngles + new Vector3(0, valueSlider, 0);
        transform.Rotate(newValue);
    }

    public void rotateZ(float valueSlider)
    {
        Vector3 newValue = transform.localEulerAngles + new Vector3(0, 0, valueSlider);
        transform.Rotate(newValue);
    }
}
