using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandCollision : MonoBehaviour {

	public GameObject ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
        Debug.Log("grabbed OnTriggerEnter");
        if (collision.collider.gameObject == ball) {
			Debug.Log ("OnCollisionEnter BallStateManager.Instance.currentBallState "+BallStateManager.Instance.currentBallState);
            if (BallStateManager.Instance.currentBallState != BallStateManager.BallState.Grabbed && BallStateManager.Instance.currentBallState != BallStateManager.BallState.Launched)
            {
                BallStateManager.Instance.Grab();
                Debug.Log("ball grabbed");

            }
		} 
	}
}
