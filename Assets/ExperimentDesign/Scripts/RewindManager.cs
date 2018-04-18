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
    bool recordingStarted = false;
    public int pointsInTimeCount = 0;
    float currentPointInTimeFloat;
    public bool isRecording = false;
    public List<GameObject> currentRewindables = new List<GameObject>();

    // TO DO: Judith, set this based on speed
    public float updateSpeed = 1;

    public double rewindRatio = 1;

    public float sliderValue = 0;

    [Header("Rewind Objects")]
    //public GameObject rewindUI;
    //public GameObject startSimulationUI;

    GameObject ballTrail;


    // Use this for initialization
    void Start()
    {
        //if(rewindUI == null)
        //    rewindUI = UIManager.Instance.rewindUI;
        //if(startSimulationUI == null)
        //startSimulationUI = UIManager.Instance.startSimulationUI;

        //if (BallPhysicsManager.Instance.isScene10) {
        //	ballTrail = BallPhysicsManager.Instance.ball.transform.Find ("Trail").gameObject;
        //	ballTrail.SetActive (false);
        //}
        currentPlayMode = PlayMode.PAUSE;
        currentPointInTimeFloat = 0;
        currentPointInTime = (int)currentPointInTimeFloat;
        maxRecordTimeInit = 2;
        maxRecordTime = maxRecordTimeInit;
        currentSpeed = PlaySpeed[2];
        UIManager.Instance.rewindUI.SetActive(false);
        //startSimulationUI.SetActive(true);
        timer = timeSimulation();
        Debug.Log("CurrentRewindables Count: " + currentRewindables.Count);
        Debug.Log("Simulation - starting rewind manager script");
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsInTimeCount > 0)
        {
            rewindRatio = (double)currentPointInTime / pointsInTimeCount;
        }

    }

    public void replay()
    {
        currentPointInTimeFloat = 0;
        currentPointInTime = (int)currentPointInTimeFloat;
        sliderValue = 0;
    }

    public void play()
    {
        currentPlayMode = PlayMode.PLAY;
        isRewinding = true;
        Debug.Log("RewindManager play");
    }

    public void pause()
    {
        currentPlayMode = PlayMode.PAUSE;
        isRewinding = false;
        Debug.Log("RewindManager pause");
    }

    public void starRecording()
    {
        Debug.Log("Simulation - start recording");
        isRecording = true;
        recordingStarted = true;
        StartCoroutine(timer);
    }

    public void setSliderValue(float value)
    {
        Debug.Log("RewindManager setSliderValue value: " + value + " total pointsInTimeCount: " + pointsInTimeCount);
        currentPointInTimeFloat = value * pointsInTimeCount;
        currentPointInTime = (int)currentPointInTimeFloat;
        sliderValue = value;
        Debug.Log("RewindManager setSliderValue currentPointInTime: " + currentPointInTime);
    }

    void FixedUpdate()
    {
        Debug.Log("RewindManager isRewinding: " + isRewinding + " ; isRecording: " + isRecording);
        if (isRewinding)
        {
            Rewind();
        }
        if (recordingStarted && !isRecording && !isRewinding)
        {
            if (currentPointInTime < pointsInTimeCount)
            {
                for (int i = 0; i < currentRewindables.Count; i++)
                {
                    Debug.Log("RewindManager currentRewindables[i] .Rewind() " + i);
                    currentRewindables[i].GetComponent<RewindableObject>().Rewind();
                }
            }
        }
        if (isRecording)
        {
            Record();
        }
    }

    public void Rewind()
    {
        Debug.Log("RewindManager rewind");
        Debug.Log("RewindManager currentPointInTime: " + currentPointInTime + " pointsInTimeCount: " + pointsInTimeCount);
        if (currentPointInTime < pointsInTimeCount)
        {
            for (int i = 0; i < currentRewindables.Count; i++)
            {
                Debug.Log("RewindManager currentRewindables[i] .Rewind() " + i);
                currentRewindables[i].GetComponent<RewindableObject>().Rewind();
            }
            UpdateCurrentPointsInTime();
        }

    }

    void UpdateCurrentPointsInTime()
    {
        updateSpeed = currentSpeed;
        Debug.Log("RewindManager UpdateCurrentPointsInTime update speed: " + updateSpeed + " currentPointInTime: " + currentPointInTime);
        currentPointInTimeFloat = currentPointInTimeFloat + updateSpeed;
        currentPointInTime = (int)currentPointInTimeFloat;
        Debug.Log("RewindManager UpdateCurrentPointsInTime updated currentPointInTime: " + currentPointInTime);
    }

    void Record()
    {
        Debug.Log("Simulation - Recording");
        Debug.Log("RewindManager currentRewindables.Count " + currentRewindables.Count);
        for (int i = 0; i < currentRewindables.Count; i++)
        {
            Debug.Log("RewindManager currentRewindables[i] .Record() " + i);
            currentRewindables[i].GetComponent<RewindableObject>().Record();
        }
        pointsInTimeCount++;
        /*if (pointsInTimeCount > Mathf.Round(maxRecordTime / Time.fixedDeltaTime))
        {
            stopRecording();
        }*/
    }

    public void Reset()
    {
        //      isRecording = true;
        //      pointsInTimeCount = 0;
        //currentPointInTimeFloat = currentPointInTime = 0;
        //GraphManager.Instance.stopGraph();
        //for (int i = 0; i < currentRewindables.Count; i++)
        //{
        //    currentRewindables[i].GetComponent<RewindableObject>().ResetRewind();
        //}

        // TO DO: Judith, set this based on speed
        updateSpeed = 1;
        rewindRatio = 1;
        sliderValue = 0;
        currentRewindables = new List<GameObject>();
        pointsInTimeCount = 0;
        isRecording = false;
        isRewinding = false;
        currentPlayMode = PlayMode.PAUSE;
        currentPointInTimeFloat = 0;
        currentPointInTime = (int)currentPointInTimeFloat;
        maxRecordTimeInit = 2;
        maxRecordTime = maxRecordTimeInit;
        currentSpeed = PlaySpeed[2];
        StrobingHandler.Instance.clearStrobes();

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
        //switchToRewindManager();
        GameStateManager.switchGameStateMode(GameStateManager.gameState.REWIND);

    }

    IEnumerator timeSimulation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            maxRecordTime--;
            Debug.Log("RewindManager timeSimulation: " + maxRecordTime);

            if (maxRecordTime == 10 && BallPhysicsManager.Instance.isScene10)
            {
                BallPhysicsManager.Instance.startStrobe = true;
                ballTrail.SetActive(true);
                GraphManager.Instance.startGraph();
            }
            if (maxRecordTime == 3 && BallPhysicsManager.Instance.isScene10)
            {
                BallPhysicsManager.Instance.breakBallFromRope();
            }
            if (maxRecordTime == 0)
            {
                stopRecording();
            }
        }


    }

    void switchToRewindManager()
    {
        //startSimulationUI.SetActive(false);
        UIManager.Instance.rewindUI.SetActive(true);
    }

}
