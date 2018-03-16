using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleForceApplier : MonoBehaviour
{

    public List<GameObject> AffectedObjects = new List<GameObject>();
    private Color currCol;
    //public Vector3 ForceVector = new Vector3(-2, 0, 0);
    private void Start()
    {
        currCol = GetComponent<MeshRenderer>().material.color;
    }


    void OnTriggerEnter(Collider collidee)

    {
        AffectedObjects.Add(collidee.gameObject);
        GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Thing entered");
    }



    void OnTriggerExit(Collider collidee)

    {
        AffectedObjects.Remove(collidee.gameObject);
        Debug.Log("Thing escaped");
        GetComponent<MeshRenderer>().material.color = currCol;

    }



    void FixedUpdate()

    {

        for (int I = 0; I < AffectedObjects.Count; I++)

        {

            if (AffectedObjects[I].GetComponent<Rigidbody>() != null)
            {
               Vector3 forceDirection = transform.position- AffectedObjects[I].transform.position;
               Debug.Log("force to center is " + forceDirection);
                if (forceDirection.magnitude < 0.1 && forceDirection.magnitude > -0.1) GameObject.Destroy(AffectedObjects[I]);
                AffectedObjects[I].GetComponent<Rigidbody>().AddForce(forceDirection.normalized  * Time.fixedDeltaTime);

                

            }

        }

    }
}
