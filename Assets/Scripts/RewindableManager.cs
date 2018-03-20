using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
public class RewindableManager : Singleton<RewindableManager> {
	float maxRecordTime=10f;
	bool isRewinding=false;
	int pointsInTimeCount=0;
	bool isRecordable=true;
	public List<RewindableObject> currentRewindables;

	public int currentPointInTime=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		if (isRewinding)
			Rewind ();
		else
			Record (); 
}
	void Rewind(){


	}
	void Record(){
		if(isRecordable){
			for (int i = 0; i < currentRewindables.Count; i++) {
				currentRewindables [i].Record ();
			}
			pointsInTimeCount++;
			if(pointsInTimeCount>Mathf.Round(maxRecordTime/Time.fixedDeltaTime)){
			}
			stopRecording ();
		}
	}
	void Reset(){
		isRecordable = true;
		pointsInTimeCount = 0;
		currentPointInTime = 0;
		for (int i = 0; i < currentRewindables.Count; i++) {
			currentRewindables [i].ResetRewind ();
		}
	}
	void stopRecording(){
		isRecordable = false;
		for (int i = 0; i < currentRewindables.Count; i++) {
			currentRewindables [i].EnableRewinding ();
		}

	}

}