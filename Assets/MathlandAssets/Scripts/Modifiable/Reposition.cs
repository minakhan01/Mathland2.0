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

    public void reposition(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.position + new Vector3(valueSlider, valueSlider, valueSlider);
        selectedObjectToModify.transform.localPosition = newValue;
    }

    public void repositionX(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localPosition + new Vector3(valueSlider, 0, 0);
        selectedObjectToModify.transform.localPosition = newValue;
    }

    public void repositionY(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localPosition + new Vector3(0, valueSlider, 0);
        selectedObjectToModify.transform.localPosition = newValue;
    }

    public void repositionZ(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localPosition + new Vector3(0, 0, valueSlider);
        selectedObjectToModify.transform.localPosition = newValue;
    }
}
