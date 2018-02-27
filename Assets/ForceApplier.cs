using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplier : MonoBehaviour
{

	public List<GameObject> AffectedObjects = new List<GameObject>();
	public float ForceVectorMagnitude = 1.0f;
	public Vector3 ForceVector = new Vector3(-1, 0, 0);

	public bool fullScaled = false;
	public void setMag(float magnitude)
	{
		ForceVectorMagnitude = magnitude;
	}

	void OnTriggerEnter(Collider collidee)

	{
		if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
		{
			if (fullScaled == false)
			{
				Color color = Color.green;
				color.a -= 0.6f;
				gameObject.GetComponent<MeshRenderer>().material.color = color;
			}
			collidee.gameObject.GetComponent<VelocityReactor>().addForceVector(gameObject);
			AffectedObjects.Add(collidee.gameObject);
			Debug.Log("Thing entered ForceField");
		}
	}
	void OnTriggerExit(Collider collidee)
	{
		if (collidee.gameObject.GetComponent<VelocityReactor>() != null)
		{
			if (fullScaled == false)
			{
				Color color = Color.blue;
				color.a -= 0.6f;
				gameObject.GetComponent<MeshRenderer>().material.color = color;
			}

			collidee.gameObject.GetComponent<VelocityReactor>().removeForceVector(gameObject);
			AffectedObjects.Remove(collidee.gameObject);
			//collidee.gameObject.GetComponent<VelocityReactor>().experiencedforce = new Vector3(0, 0, 0);
			Debug.Log("Thing escaped ForceField");
		}
	}

	void OnDestroy()
	{
		for(int i = 0; i < AffectedObjects.Count; i++)
		{
			AffectedObjects[i].GetComponent<VelocityReactor>().removeForceVector(gameObject);
		}
	}
}