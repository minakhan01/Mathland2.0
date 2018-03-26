using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class RewindManager : Singleton<RewindManager>
{

    public enum RewindMode { ON, OFF };
    public enum PlayMode { PLAY, PAUSE };
    public readonly float[] PlaySpeed = { 0.5f, 0.25f, 1f, 2f, 4f };

    public float currentSpeed { get; set; }
    public PlayMode currentPlayMode { get; set; }


    public int currentPointInTime { get; set; }
	public int maxRecordTime, maxRecordTimeInit;
    IEnumerator timer;
    public bool isRewinding = false;
    int pointsInTimeCount = 0;
    bool isRecording = false;
	public List<GameObject> currentRewindables = new List<GameObject>();

    // TO DO: Judith, set this based on speed
    public float updateSpeed = 1;

	public double rewindRatio = 1;

	public float sliderValue = 0;

    [Header("Rewind Objects")]
    public GameObject rewindUI;
    public GameObject startSimulationUI;


    // Use this for initialization
    void Start()
    {
        currentPlayMode = PlayMode.PAUSE;
        currentPointInTime = 0;
		maxRecordTime = maxRecordTimeInit;
        currentSpeed = PlaySpeed[2];
        rewindUI.SetActive(false);
        startSimulationUI.SetActive(true);
        timer = timeSimulation();
		Debug.Log ("CurrentRewindables Count: " + currentRewindables.Count);
        Debug.Log("Simulation - starting rewind manager script");
    }

    // Update is called once per frame
    void Update()
    {
		if (pointsInTimeCount > 0) {
			rewindRatio = (double)currentPointInTime / pointsInTimeCount;
		}
    }

    public void replay()
    {
        currentPointInTime = 0;
    }

    public void play()
    {
        currentPlayMode = PlayMode.PLAY;
		isRewinding = true;
		Debug.Log ("RewindManager play");
    }

    public void pause()
    {
        currentPlayMode = PlayMode.PAUSE;
		isRewinding = false;
		Debug.Log ("RewindManager pause");
    }

    public void starRecording()
    {
        Debug.Log("Simulation - start recording");
        isRecording = true;
        StartCoroutine(timer);
    }

    public void setSliderValue(float value)
    {
		Debug.Log ("RewindManager setSliderValue value: "+value+" total pointsInTimeCount: "+pointsInTimeCount);
		currentPointInTime = (int)(value * pointsInTimeCount);
		sliderValue = value;
		Debug.Log ("RewindManager setSliderValue currentPointInTime: "+currentPointInTime);
    }

    void FixedUpdate()
    {
		Debug.Log ("RewindManager isRewinding: "+isRewinding+" ; isRecording: "+ isRecording);
		if (isRewinding) {
			Rewind ();
		}
		if (isRecording) {
			Record ();
		}
    }

    public void Rewind()
    {
		Debug.Log ("RewindManager rewind");
		Debug.Log ("RewindManager currentPointInTime: "+ currentPointInTime+" pointsInTimeCount: "+pointsInTimeCount);
        if (currentPointInTime < pointsInTimeCount)
        {
            for (int i = 0; i < currentRewindables.Count; i++)
            {
				Debug.Log ("RewindManager currentRewindables[i] .Rewind() "+i);
                currentRewindables[i].GetComponent<RewindableObject>().Rewind();
            }
            UpdateCurrentPointsInTime();
        }

    }

    void UpdateCurrentPointsInTime()
    {
//		updateSpeed = currentSpeed;
//		Debug.Log ("RewindManager update speed: " + updateSpeed);
        currentPointInTime = currentPointInTime + Mathf.RoundToInt(1f * updateSpeed);
    }

    void Record()
    {
        Debug.Log("Simulation - Recording");
		Debug.Log ("RewindManager currentRewindables.Count "+ currentRewindables.Count);
        for (int i = 0; i < currentRewindables.Count; i++)
        {
			Debug.Log ("RewindManager currentRewindables[i] .Record() "+i);
            currentRewindables[i].GetComponent<RewindableObject>().Record();
        }
        pointsInTimeCount++;
        /*if (pointsInTimeCount > Mathf.Round(maxRecordTime / Time.fixedDeltaTime))
        {
            stopRecording();
        }*/
    }

    void Reset()
    {
        isRecording = true;
        pointsInTimeCount = 0;
        currentPointInTime = 0;
        GraphManager.Instance.stopGraph();
        for (int i = 0; i < currentRewindables.Count; i++)
        {
            currentRewindables[i].GetComponent<RewindableObject>().ResetRewind();
        }
    }

    void stopRecording()
    {
        Debug.Log("Simulation - stop recording");
        StopCoroutine(timer);
        isRecording = false;
		maxRecordTime = maxRecordTimeInit;
        BallPhysicsManager.Instance.stopBallPhysics();
        for (int i = 0; i < currentRewindables.Count; i++)
        {
            currentRewindables[i].GetComponent<RewindableObject>().EnableRewinding();
        }
        switchToRewindManager();

    }

    IEnumerator timeSimulation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            maxRecordTime--;
			Debug.Log("RewindManager timeSimulation: " + maxRecordTime);

            if (maxRecordTime == 0)
            {
                stopRecording();
            }
        }


    }

    void switchToRewindManager()
    {
        startSimulationUI.SetActive(false);
        rewindUI.SetActive(true);
    }

}
