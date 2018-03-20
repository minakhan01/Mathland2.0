
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsRewindUI : MonoBehaviour
{

    [Header("Sprites")]
    public Sprite pauseButton;
    public Sprite playButton;

    [Header("ButtonsProperties")]
    public ButtonProperties playPauseButton;
    public ButtonProperties replayButton;
    public ButtonProperties speedButton;


    public GameObject speedMenu;
    public Text speedMenuText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playPauseButtonHandler()
    {
        playPauseButton.switchState();
        if (RewindManager.Instance.currentPlayMode == RewindManager.PlayMode.PLAY)
        {
            RewindManager.Instance.currentPlayMode = RewindManager.PlayMode.PAUSE;
            RewindManager.Instance.pause();
        }
        else
        {
            RewindManager.Instance.currentPlayMode = RewindManager.PlayMode.PLAY;
            RewindManager.Instance.play();
        }
    }

    public void replayButtonHandler()
    {
        RewindManager.Instance.replay();
    }

    public void speedButtonHandler()
    {
        speedMenu.GetComponent<ButtonProperties>().switchState();
        speedMenu.SetActive(true);
    }

    public void doubleSlowSpeedButtonHandler()
    {
        RewindManager.Instance.currentSpeed = RewindManager.Instance.PlaySpeed[0];
        updateSpeedButton();
    }

    public void quarterSlowSpeedButtonHandler()
    {
        RewindManager.Instance.currentSpeed = RewindManager.Instance.PlaySpeed[1];
        updateSpeedButton();
    }

    public void normalSpeedButtonHandler()
    {
        RewindManager.Instance.currentSpeed = RewindManager.Instance.PlaySpeed[2];
        updateSpeedButton();
    }

    public void doubleFastSpeedButtonHandler()
    {
        RewindManager.Instance.currentSpeed = RewindManager.Instance.PlaySpeed[3];
        updateSpeedButton();
    }

    public void fourTimesFastSpeedButtonHandler()
    {
        RewindManager.Instance.currentSpeed = RewindManager.Instance.PlaySpeed[4];
        updateSpeedButton();
    }

    void updateSpeedButton () {
        speedMenu.SetActive(false);
        speedMenuText.text = "x" + RewindManager.Instance.currentSpeed.ToString();
    }


}
