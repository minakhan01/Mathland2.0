using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPredictorScript : MonoBehaviour {
	//create a trajectory predictor in code
	TrajectoryPredictor tp;
    public Vector3 force_exp= new Vector3(0, 0, 0);
    public Vector3 velocity_of_ball = new Vector3(0, 0, 0);
    public GameObject objToLaunch;
	public Transform launchPoint;
	// Use this for initialization
	void Start () {
		tp = GetComponent<TrajectoryPredictor>();
        tp.drawDebugOnPrediction = true;
    }
	
	// Update is called once per frame
	void Update () {
		launchPoint.transform.position = objToLaunch.transform.position;
		launchPoint.transform.rotation = objToLaunch.transform.rotation;
        //Debug.Log("launchPoint.transform.position: "+ launchPoint.transform.position);
        //Debug.Log("launchPoint.transform.rotation: " + launchPoint.transform.rotation);
        
    }
    public void resetBall()
    {
        force_exp = new Vector3(0, 0, 0);
        velocity_of_ball = new Vector3(0, 0, 0);
    }
	void LateUpdate()
	{
		//if (BallStateManager.Instance.currentBallState == BallStateManager.BallState.Grabbed)
		{
            //  Debug.Log("late  update");
            //            tp.donKillLine = true;
            //set line duration to delta time so that it only lasts the length of a frame

            //ForceApplier[] forcefields = FindObjectsOfType(typeof(ForceApplier)) as ForceApplier[];
            //foreach (ForceApplier forcefield in forcefields)
            //{
            //    forcefield.applyForce();
            //}
            if (objToLaunch == null) Debug.Log("objToLaunch is null");
            else if (objToLaunch.GetComponent<VelocityReactor>() != null) {
                objToLaunch.GetComponent<VelocityReactor>().updateVelocityandForce();
				velocity_of_ball = objToLaunch.GetComponent<VelocityReactor>().updateInitVelocity;
                force_exp = objToLaunch.GetComponent<VelocityReactor>().updateExperiencedForce;
                
            }
            else
            {
                resetBall();
            }

            tp.debugLineDuration = Time.unscaledDeltaTime;
            //tell the predictor to predict a 3d line. this will also cause it to draw a prediction line
            //because drawDebugOnPredict is set to true
            //tp.Predict3D(launchPoint.position, launchPoint.forward * PhysicsManager.Instance.force, Physics.gravity);
            //Debug.Log("Trakectory force: " + force_exp);

            tp.Predict3D(launchPoint.position,velocity_of_ball, force_exp);
            //Apply the forces and velocities
            Rigidbody rbi = objToLaunch.GetComponent<Rigidbody>();
            //rbi.velocity = objToLaunch.GetComponent<VelocityReactor>().initialvel;
            if(rbi.isKinematic)rbi.AddForce(objToLaunch.GetComponent<VelocityReactor>().experiencedforce);
            //Debug.Log("Hit Object: " + tp.hitInfo3D.collider.gameObject.name);

            //this static method can be used as well to get line info without needing to have a component and such
            //TrajectoryPredictor.GetPoints3D(launchPoint.position, launchPoint.forward * force, Physics.gravity);

            ////info text stuff
            //if (infoText)
            //{
            //    //this will check if the predictor has a hitinfo and then if it does will update the onscreen text
            //    //to say the name of the object the line hit;
            //    if (tp.hitInfo3D.collider)
            //        infoText.text = "Hit Object: " + tp.hitInfo3D.collider.gameObject.name;
            //}
        }

	}
}
