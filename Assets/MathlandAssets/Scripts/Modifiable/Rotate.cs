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

    public void rotate (GameObject selectedObjectToModify, float valueSlider) {
        Vector3 newValue = selectedObjectToModify.transform.localEulerAngles + new Vector3(valueSlider, valueSlider, valueSlider);
        selectedObjectToModify.transform.Rotate(newValue);
    }

    public void rotateX(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localEulerAngles + new Vector3(valueSlider, 0, 0);
        selectedObjectToModify.transform.Rotate(newValue);
    }

    public void rotateY(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localEulerAngles + new Vector3(0, valueSlider, 0);
        selectedObjectToModify.transform.Rotate(newValue);
    }

    public void rotateZ(GameObject selectedObjectToModify, float valueSlider)
    {
        Vector3 newValue = selectedObjectToModify.transform.localEulerAngles + new Vector3(0, 0, valueSlider);
        selectedObjectToModify.transform.Rotate(newValue);
    }
}
