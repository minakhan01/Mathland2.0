using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityReactor : MonoBehaviour {
    // Use this for initialization
    public Vector3 initialvel = new Vector3(0, 0, 0);
    public Vector3 experiencedforce= new Vector3(0, 0, 0);
    private List<GameObject> velocities = new List<GameObject>();
    private List<GameObject> forces = new List<GameObject>();
    public GameObject fullScaledforce;
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
        
        Vector3 total_velocity = new Vector3(0, 0, 0);
        experiencedforce= new Vector3(0, 0, 0);
        //if (velocities == null) Debug.Log("veloicities is null");
        for (int i = 0; i < velocities.Count; i++)
        {
            //Debug.Log("THIS RUNS VELZONE" + i);
            GameObject curr_velvect = velocities[i];
            //Debug.Log("The currvect for index "+i+" is "+curr_velvect);
            float size = curr_velvect.transform.localScale.x*5;
            Vector3 VelocityVector =-1*size * curr_velvect.transform.right.normalized;
            total_velocity += VelocityVector;

        }
        //if (velocities == null) Debug.Log("veloicities is null");
        for (int i = 0; i < forces.Count; i++)
        {
            //Debug.Log("THIS RUNS FORCEZONE"+i);
            GameObject curr_forvect = forces[i];
            GameObject reparrow = curr_forvect.GetComponent<ArrowManager>().representativearrow;
            //if (reparrow == null) Debug.Log("reparrow is null bruh");
            Vector3 FDirection = -1 * reparrow.transform.right.normalized;
            float magnitude = reparrow.transform.localScale.x * 5;
            experiencedforce = magnitude * FDirection;
            Physics.gravity = new Vector3(0,0,0);

            //Debug.Log("Normal force of " + experiencedforce + "applies.");
        }
        if (fullScaledforce != null)
        {
            GameObject reparrow = fullScaledforce.GetComponent<SliderReactor>().child.GetComponent<ArrowManager>().representativearrow;
            if (reparrow == null) Debug.Log("reparrow is null bruh fullscalezone");
            Vector3 FDirection = -1 * reparrow.transform.right.normalized;
            float magnitude = reparrow.transform.localScale.x * 10;
            experiencedforce = magnitude * FDirection;
            //Debug.Log("VelocityReactor force: " + experiencedforce); 
            Physics.gravity = experiencedforce;

            //Debug.Log("Fullscaled force of " + experiencedforce + "applies.");
        }
        initialvel = total_velocity;


        //transform.gameObject.GetComponent<Rigidbody>().velocity=initialvel;
    }


}
