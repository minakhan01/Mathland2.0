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

    public void resize(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localScale + new Vector3(valueSlider, valueSlider, valueSlider);
        selectedObjectToModify.transform.localScale = newValue;
    }

    public void resizeX(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localScale + new Vector3(valueSlider, 0, 0);
        selectedObjectToModify.transform.localScale = new Vector3(valueSlider, 0, 0);
    }

    public void resizeY(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localScale + new Vector3(0, valueSlider, 0);
        selectedObjectToModify.transform.localScale = newValue;
    }

    public void resizeZ(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localScale + new Vector3(0, 0, valueSlider);
        selectedObjectToModify.transform.localScale = newValue;
    }
}
