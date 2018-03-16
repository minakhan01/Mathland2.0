using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallInformation : MonoBehaviour {


    public GameObject BallTextObject;
    private TextMesh ballText;
    private float ballVelocity;

	// Use this for initialization
	void Start () {
         //ballText = BallTextObject.GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setVelocity(float velocity) {
        // Don't show ball velocity anymore
        return; 
        ballText = BallTextObject.GetComponent<TextMesh>();
        ballText.text = ""+velocity;
        ballVelocity = velocity;
    }
}
