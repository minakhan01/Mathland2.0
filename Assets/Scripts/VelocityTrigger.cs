using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTrigger : MonoBehaviour {
    public Vector3 VelocityVector=new Vector3(0,0,0);
    public float VelocityVectorMagnitude=1.0f;
    public GameObject BALL;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collidee)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        //GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Active");
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			VelocityResponse.Instance.addVelocityVector (gameObject);
		}
		else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name){
			VelocityResponseBallTwo.Instance.addVelocityVector (gameObject);
		}
		Debug.Log ("VelocityVector and ball collided!");
    }
    private void OnTriggerExit(Collider collidee)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
		if (collidee.gameObject.name == BallPhysicsManager.Instance.ball.name) {
			VelocityResponse.Instance.removeVelocityVector (gameObject);
		}
		else if (collidee.gameObject.name == BallPhysicsManager.Instance.ballTwo.name){
			VelocityResponseBallTwo.Instance.removeVelocityVector (gameObject);
		}
		Debug.Log ("VelocityVector and ball collision removed");
    }
    private void OnDestroy()
    {
		VelocityResponse.Instance.removeVelocityVector(gameObject);
    }
}
