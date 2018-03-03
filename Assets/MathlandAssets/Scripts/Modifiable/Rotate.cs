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

	float convertSliderValue(float originalValue) {
		float convertedValue = (originalValue + 0.5f)*2*Mathf.PI - Mathf.PI;
		Debug.Log ("Converted value " + convertedValue);
		//float convertedValue = (originalValue)*2*Mathf.PI;
		return convertedValue;
	}

    public void rotate (float valueSlider) {
		float convertedValue = convertSliderValue (valueSlider);
		transform.Rotate(new Vector3(convertedValue, convertedValue, convertedValue));
    }

    public void rotateX(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		transform.Rotate(new Vector3(convertedValue, 0, 0));
    }

    public void rotateY(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		transform.Rotate(new Vector3(0, convertedValue, 0));
    }

    public void rotateZ(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		transform.Rotate(new Vector3(0, 0, convertedValue));
    }
}
