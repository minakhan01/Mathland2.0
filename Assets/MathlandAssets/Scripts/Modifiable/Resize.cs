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

	private float minMaxValue(float val)
	{
		if (val < 0.1f) // minimum boundary
			return 0.1f;
		else if (val > 1.5f) // maximum boundary
			return 1.5f;
		else
			return val;
	}

    public void resize(float valueSlider)
    {
		if (hasRopeResizeComponent ()) {
			resizeRope (valueSlider);
			return;
		}
		Vector3 newValue =new Vector3(minMaxValue(transform.localScale.x*convertSliderValue(valueSlider)) , minMaxValue(transform.localScale.y*convertSliderValue(valueSlider)), minMaxValue(transform.localScale.z*convertSliderValue(valueSlider)));
        transform.localScale = newValue;
    }

    public void resizeX(float valueSlider)
    {
		if (hasRopeResizeComponent ()) {
			resizeRope (valueSlider);
			return;
		}
		Vector3 newValue = new Vector3(minMaxValue(transform.localScale.x*convertSliderValue(valueSlider)) , transform.localScale.y, transform.localScale.z);
		transform.localScale = newValue;
    }

    public void resizeY(float valueSlider)
    {
		if (hasRopeResizeComponent ()) {
			resizeRope (valueSlider);
			return;
		}
		Vector3 newValue = new Vector3(transform.localScale.x , minMaxValue(transform.localScale.y*convertSliderValue(valueSlider)), transform.localScale.z);
        transform.localScale = newValue;
    }

    public void resizeZ(float valueSlider)
    {
		if (hasRopeResizeComponent ()) {
			resizeRope (valueSlider);
			return;
		}
		Vector3 newValue = new Vector3(transform.localScale.x , transform.localScale.y, minMaxValue(transform.localScale.z*convertSliderValue(valueSlider)));
        transform.localScale = newValue;
    }

	float convertSliderValue(float originalValue) {
		float convertedValue = (originalValue + 0.5f)*2f;
		return convertedValue;
	}

	void resizeRope (float value) {
		return;
	}

	bool hasRopeResizeComponent() {
		if(transform.GetComponent<RopeResize>() != null)
		{
			return true;
		}
		return false;
	}
}
