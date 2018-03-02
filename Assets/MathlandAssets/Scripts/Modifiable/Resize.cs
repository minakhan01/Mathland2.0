using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resize(float valueSlider)
    {
        Vector3 newValue = transform.localScale + new Vector3(valueSlider, valueSlider, valueSlider);
        transform.localScale = newValue;
    }

    public void resizeX(float valueSlider)
    {
        Vector3 newValue = transform.localScale + new Vector3(valueSlider, 0, 0);
        transform.localScale = new Vector3(valueSlider, 0, 0);
    }

    public void resizeY(float valueSlider)
    {
        Vector3 newValue = transform.localScale + new Vector3(0, valueSlider, 0);
        transform.localScale = newValue;
    }

    public void resizeZ(float valueSlider)
    {
        Vector3 newValue = transform.localScale + new Vector3(0, 0, valueSlider);
        transform.localScale = newValue;
    }
}
