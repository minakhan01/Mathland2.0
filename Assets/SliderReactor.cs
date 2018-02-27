using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderReactor : MonoBehaviour {
    public float magnitude;
    public GameObject child;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setMagnitude(float mag)
    {
        if (child.GetComponent<ArrowManager>() != null)
        {
            child.GetComponent<ArrowManager>().setScale(mag);
            child.GetComponent<ArrowManager>().LegoControllerTrigger();
        }
            if (child.GetComponent<ForceApplier>() != null)
            child.GetComponent<ForceApplier>().setMag(mag);

    }
}
