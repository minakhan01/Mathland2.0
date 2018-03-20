using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class RewindManager : Singleton<RewindManager>
{

    public enum RewindMode { ON, OFF };
    public enum PlayMode { PLAY, PAUSE };
    public readonly float[] PlaySpeed = {0.5f, 0.25f, 1f, 2f, 4f};

    public float currentSpeed { get; set; }
    public PlayMode currentPlayMode { get; set; }
    public float currentTime { get; set; }

    [Header("Rewind Objects")]
    public GameObject rewindUI;


    // Use this for initialization
    void Start()
    {
        currentTime = 0f;
        currentSpeed = 1f;
        rewindUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void replay()
    {
        currentTime = 0f;
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

}
