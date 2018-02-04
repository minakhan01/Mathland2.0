using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour {

	void OnMouseDown()
	{
		PinchScale.ScaleTransform = this.transform;
	}
}
