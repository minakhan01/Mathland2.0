using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour {

    public GameObject confetti;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.gameObject.name.Equals("Ball")) {
            Debug.Log("Ball collided! Goal");
            confetti.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Launch confetti");
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
