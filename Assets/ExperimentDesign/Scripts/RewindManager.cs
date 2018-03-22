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
    int maxRecordTime = 5;
    IEnumerator timer;
    bool isRewinding = false;
    int pointsInTimeCount = 0;
    bool isRecording = false;
    public List<RewindableObject> currentRewindables;

    // TO DO: Judith, set this based on speed
    public float updateSpeed = 1;

    [Header("Rewind Objects")]
    public GameObject rewindUI;
    public GameObject startSimulationUI;


    // Use this for initialization
    void Start()
    {
        currentPlayMode = PlayMode.PAUSE;
        currentPointInTime = 0;
        currentSpeed = PlaySpeed[2];
        rewindUI.SetActive(false);
        startSimulationUI.SetActive(true);
        timer = timeSimulation();
        Debug.Log("Simulation - starting rewind manager script");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void replay()
    {
        currentPointInTime = 0;
    }

    public void play()
    {
        currentPlayMode = PlayMode.PLAY;
    }

    public void pause()
    {
        currentPlayMode = PlayMode.PAUSE;
    }

    public void starRecording()
    {
        Debug.Log("Simulation - start recording");
        isRecording = true;
        StartCoroutine(timer);
    }

    public void setSliderValue(float value)
    {
        currentPointInTime = (int)value * pointsInTimeCount;
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else if (isRecording)
            Record();
    }

    public void Rewind()
    {
        if (currentPointInTime < pointsInTimeCount)
        {
            for (int i = 0; i < currentRewindables.Count; i++)
            {
                currentRewindables[i].GetComponent<RewindableObject>().Rewind();
            }
            UpdateCurrentPointsInTime();
        }

    }

    void UpdateCurrentPointsInTime()
    {
        currentPointInTime = currentPointInTime + Mathf.RoundToInt(1f * updateSpeed);
    }

    void Record()
    {
        //Debug.Log("Simulation - Recording");
        for (int i = 0; i < currentRewindables.Count; i++)
        {
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
        maxRecordTime = 10;
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
            Debug.Log("Simulation - time: " + maxRecordTime);


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
