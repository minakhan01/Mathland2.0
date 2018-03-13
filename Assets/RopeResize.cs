using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeResize : MonoBehaviour {
	public ObiRope rope;

	ObiRopeCursor cursor;
//	ObiRope rope;

	// Use this for initialization
	void Start () {
		cursor = GetComponentInChildren<ObiRopeCursor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resizeRope (float value) {
		cursor.ChangeLength(rope.RestLength * (value + 0.5f));
	}
}
