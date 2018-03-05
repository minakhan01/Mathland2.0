using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityReactor : MonoBehaviour {
    // Use this for initialization
    public Vector3 objectInitVelocity = new Vector3(0, 0, 0);
    public Vector3 experiencedForce = new Vector3(0, 0, 0);

    public List<GameObject> velocities = new List<GameObject>();
    public List<GameObject> forces = new List<GameObject>();
    public GameObject fullScaledforce;
	public Vector3 updateInitVelocity = new Vector3(0, 0, 0);
	public Vector3 updateExperiencedForce = new Vector3(0, 0, 0);


    void Start () {
        GetComponent<Rigidbody>().isKinematic=true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addVelocityVector(GameObject gb)
    {
        velocities.Add(gb);
        Debug.Log("New velocity added now there are " + velocities.Count + " velocity vectors attached");
    }
    public void removeVelocityVector(GameObject gb)
    {
        velocities.Remove(gb);
        Debug.Log("velocity removed now there are " + velocities.Count + " velocity vectors attached");
    }
    public void addForceVector(GameObject gb)
    {
        forces.Add(gb);
        Debug.Log("New force added now there are " + forces.Count + " force vectors attached");
    }
    public void removeForceVector(GameObject gb)
    {
        forces.Remove(gb);
        Debug.Log("force removed now there are " + forces.Count + " force vectors attached");
    }


	public void updateVelocityandForce()
    {
        //initialize the total velocity and experienced force which represents
		//ball's vel / force after all game tool interactions
       



		//start by looping through all game objects that contribute to velocity
        foreach (GameObject velocityAffectingGameObject in velocities)
        {
			//get the magnitude of velocity arrow
			float magnitudeCurrentForceVelocity = velocityAffectingGameObject.transform.localScale.x * 5; 

			//impose this magnitude on the direction of the arrow
			Vector3 VelocityVector = - magnitudeCurrentForceVelocity * 
				velocityAffectingGameObject.transform.right.normalized;

			//add this to the ball's total velocity
			updateInitVelocity += VelocityVector;
        }

		//set our gameobject's initial velocity to be the total velocity of gameobjects acting on it
		objectInitVelocity = updateInitVelocity;



		//loop through all game objects that apply a force on the ball
		foreach (GameObject forceAffectingGameObject in forces)
        {
			// get magnitude and direction of the current force affecting our object
			GameObject representationArrow = forceAffectingGameObject.GetComponent<ArrowManager>().representativearrow;
			float magnitudeCurrentForceVector = representationArrow.transform.localScale.x * 5;
			Vector3 directionCurrentForceVector = - representationArrow.transform.right.normalized;

			//calculate the experienced force vector... and add it to the net Force
			updateExperiencedForce = magnitudeCurrentForceVector * directionCurrentForceVector;
			experiencedForce += updateExperiencedForce;

        }



		//what is fullScaledforce?? why is this a public gameobject??
        if (fullScaledforce != null)
        {
			//assuming this is a unique gameobject that applies force, so get its arrow representation
            GameObject reparrow = 
				fullScaledforce.GetComponent<SliderReactor>().child.GetComponent<ArrowManager>().representativearrow;
            
			//get the direction and magnitude of this unique gameobject's effects on force
			Vector3 directionCurrentForceVector = -1 * reparrow.transform.right.normalized;
			float magnitudeCurrentForceVector = reparrow.transform.localScale.x * 10;

			//calculate the entire experience force vector and add it to the Net Experienced Force
			updateExperiencedForce = magnitudeCurrentForceVector * directionCurrentForceVector;
			experiencedForce += updateExperiencedForce;
        }
			
			
    }


}
