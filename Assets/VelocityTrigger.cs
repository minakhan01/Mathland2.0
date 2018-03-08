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
        if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
        {
            BALL = collidee.gameObject;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            //GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Active");
			VelocityResponse.Instance.addVelocityVector(gameObject);
			Debug.Log ("VelocityVector and ball collided!");
        }
    }
    private void OnTriggerExit(Collider collidee)
    {
        if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
        {
            //BALL = null;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
			VelocityResponse.Instance.removeVelocityVector(gameObject);
			Debug.Log ("VelocityVector and ball collision removed");
            //GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("Velocity Vector Inactive");
        }
    }
    private void OnDestroy()
    {
		VelocityResponse.Instance.removeVelocityVector(gameObject);
    }
}
