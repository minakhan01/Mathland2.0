
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsRewindUI : MonoBehaviour
{

    [Header("Sprites")]
    public Sprite pauseButtonSprite;
    public Sprite playButtonSprite;

    public ButtonProperties speedButton;
    public Slider rewindSlider;
    public Image playPauseImage;
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
        if (RewindManager.Instance.currentPlayMode == RewindManager.PlayMode.PLAY)
        {
            RewindManager.Instance.currentPlayMode = RewindManager.PlayMode.PAUSE;
            RewindManager.Instance.pause();
            setPlayButtonSprite(pauseButtonSprite);
        }
        else
        {
            RewindManager.Instance.currentPlayMode = RewindManager.PlayMode.PLAY;
            RewindManager.Instance.play();
            setPlayButtonSprite(playButtonSprite);
        }
    }

    public void replayButtonHandler()
    {
        RewindManager.Instance.replay();
    }

    public void speedButtonHandler()
    {
        speedButton.switchState();
        speedMenu.SetActive(!speedMenu.activeSelf);
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

    public void rewindSliderHandler() {
        RewindManager.Instance.setSliderValue(rewindSlider.value);
    }

    void updateSpeedButton () {
        speedMenu.SetActive(false);
        speedMenuText.text = "x" + RewindManager.Instance.currentSpeed.ToString();
    }

    void setPlayButtonSprite(Sprite sprite) {
        playPauseImage.sprite = sprite;
    }

}
