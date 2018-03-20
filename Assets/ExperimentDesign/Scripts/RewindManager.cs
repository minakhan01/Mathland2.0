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
    float maxRecordTime = 10f;
    bool isRewinding = false;
    int pointsInTimeCount = 0;
    bool isRecordable = true;
    public List<RewindableObject> currentRewindables;

    [Header("Rewind Objects")]
    public GameObject rewindUI;


    // Use this for initialization
    void Start()
    {
        currentPointInTime = 0;
        currentSpeed = 1f;
        //TODO: rewindUI.SetActive(false);

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

    public void setRewindUIActiveAgain()
    {
        rewindUI.SetActive(true);
    }

    public void setSliderValue(float value) {
        currentPointInTime = (int) value * pointsInTimeCount;
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {


    }

    void Record()
    {
        if (isRecordable)
        {
            for (int i = 0; i < currentRewindables.Count; i++)
            {
                currentRewindables[i].Record();
            }
            pointsInTimeCount++;
            if (pointsInTimeCount > Mathf.Round(maxRecordTime / Time.fixedDeltaTime))
            {
            }
            stopRecording();
        }
    }

    void Reset()
    {
        isRecordable = true;
        pointsInTimeCount = 0;
        currentPointInTime = 0;
        for (int i = 0; i < currentRewindables.Count; i++)
        {
            currentRewindables[i].ResetRewind();
        }
    }

    void stopRecording()
    {
        isRecordable = false;
        for (int i = 0; i < currentRewindables.Count; i++)
        {
            currentRewindables[i].EnableRewinding();
        }
    }

}
