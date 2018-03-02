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
		float convertedValue = convertSliderValue (valueSlider);
		Vector3 newValue = transform.position + new Vector3(convertedValue, convertedValue, convertedValue);
        transform.localPosition = newValue;
    }

    public void repositionX(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		Vector3 newValue = transform.localPosition + new Vector3(convertedValue, 0, 0);
        transform.localPosition = newValue;
    }

    public void repositionY(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		Vector3 newValue = transform.localPosition + new Vector3(0, convertedValue, 0);
        transform.localPosition = newValue;
    }

    public void repositionZ(float valueSlider)
    {
		float convertedValue = convertSliderValue (valueSlider);
		Vector3 newValue = transform.localPosition + new Vector3(0, 0, convertedValue);
        transform.localPosition = newValue;
    }

	float convertSliderValue(float originalValue) {
		float convertedValue = (originalValue + 0.5f)*2f - 1f;
		return convertedValue;
	}
}
