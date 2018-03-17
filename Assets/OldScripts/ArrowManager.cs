using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {


	public GameObject arrow;
	public float ArrowManagerScale=0.01f;
	public Quaternion ArrowRotation=new Quaternion(0,0,0,0);
	public GameObject representativearrow;
	// Use this for initialization

	void Start()
	{
		//on creation give a unique name based on the order of creation
		LegoControllerTrigger();
	}
	// Update is called once per frame
	void Update()
	{
	}


	public void setScale(float mag)
	{
		ArrowManagerScale = mag;
	}
	public void updateRotation(Quaternion rotationValue)
	{
		ArrowRotation = rotationValue;
		Debug.Log("Rotation set to " + rotationValue);
	}
	public void LegoControllerTrigger()
	{
		//shrink down the huge arrow

		arrow.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f)*ArrowManagerScale;
		arrow.transform.rotation = ArrowRotation;
		//Destroy all children
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		//Get the size of the forceItem object now
		float ForceBoxSize = transform.localScale.x;
		Debug.Log("ForceBoxSize is " + ForceBoxSize);
		int numOfArrows=(int)Mathf.Lerp(1,6,ForceBoxSize/4);
		float spacing = ForceBoxSize / numOfArrows;
		Debug.Log("Arrows num is " + numOfArrows);
		//TODO
		int arrowcounter = 0;
		for(int i = -numOfArrows / 2; i <= numOfArrows / 2; i++)
		{
			for (int j = -numOfArrows / 2; j <= numOfArrows / 2; j++)
			{
				for (int k = -numOfArrows / 2; k <= numOfArrows / 2; k++)
				{
					Vector3 newpos = new Vector3(i * spacing, j * spacing, k * spacing);
					GameObject createdarrow=Instantiate(arrow, transform.position+newpos, ArrowRotation, transform);
					if (arrowcounter == 0)
					{
						representativearrow = createdarrow;
					}
					arrowcounter++;
				}
			}
		}

		Debug.Log("Arrows updated");
	}
}