using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void reposition(float valueSlider)
    {
        Vector3 newValue = transform.position + new Vector3(valueSlider, valueSlider, valueSlider);
        transform.localPosition = newValue;
    }

    public void repositionX(float valueSlider)
    {
        Vector3 newValue = transform.localPosition + new Vector3(valueSlider, 0, 0);
        transform.localPosition = newValue;
    }

    public void repositionY(float valueSlider)
    {
        Vector3 newValue = transform.localPosition + new Vector3(0, valueSlider, 0);
        transform.localPosition = newValue;
    }

    public void repositionZ(float valueSlider)
    {
        Vector3 newValue = transform.localPosition + new Vector3(0, 0, valueSlider);
        transform.localPosition = newValue;
    }
}
