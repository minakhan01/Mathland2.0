using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

	private Vector3 initialScale; 

	// Use this for initialization
	void Start () {
		// record initial scale, use this as a basis
		initialScale = transform.localScale; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resize(float valueSlider)
    {
		Vector3 newValue = transform.localScale*convertSliderValue(valueSlider);
        transform.localScale = newValue;
    }

    public void resizeX(float valueSlider)
    {
		Vector3 newValue = new Vector3(transform.localScale.x*convertSliderValue(valueSlider) , transform.localScale.y, transform.localScale.z);
		transform.localScale = newValue;
    }

    public void resizeY(float valueSlider)
    {
		Vector3 newValue = new Vector3(transform.localScale.x , transform.localScale.y*convertSliderValue(valueSlider), transform.localScale.z);
        transform.localScale = newValue;
    }

    public void resizeZ(float valueSlider)
    {
		Vector3 newValue = new Vector3(transform.localScale.x , transform.localScale.y, transform.localScale.z*convertSliderValue(valueSlider));
        transform.localScale = newValue;
    }

	float convertSliderValue(float originalValue) {
		float convertedValue = (originalValue + 0.5f)*2f;
		return convertedValue;
	}
}
