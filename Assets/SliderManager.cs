using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SliderManager : Singleton<SliderManager>
{

    public GameObject Slider;
    private SliderGestureControl sliderControl;

    KeywordRecognizer keywordRecognizer;
    string showSliderCommand = "Change Magnitude";
    string hideSliderCommand = "Fix Magnitude";

    bool isUpdatingSldier = false;
    SliderReactor sliderReactor = null;

    public float magnitude = 1;

    GameObject controlledObject = null;

    // Use this for initialization
    void Start () {
        Slider.SetActive(false);
        sliderControl = Slider.GetComponent<SliderGestureControl>(); 
        setupTestKeywords();
	}

    void setupTestKeywords()
    {

        // Setup a keyword recognizer to enable resetting the target location.
        List<string> keywords = new List<string>();

        keywords.Add(showSliderCommand);
        keywords.Add(hideSliderCommand);

        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }

    /// <summary>
    /// When the keyword recognizer hears a command this will be called.
    /// In this case we only have one keyword, which will re-enable moving the
    /// target.
    /// </summary>
    /// <param name="args">information to help route the voice command.</param>
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        string stringDetected = args.text.ToLower();
        if (stringDetected.Equals(showSliderCommand.ToLower()))
        {
            Debug.Log("show slider command");
            controlledObject = GestureManager.Instance.focusedObject;
            sliderReactor = controlledObject.GetComponent<SliderReactor>();
            if (sliderReactor != null)
            {
                Debug.Log("slider reactor exists");
                Slider.SetActive(true);
                Slider.transform.position = Camera.main.transform.forward * 1 + Camera.main.transform.position;
                isUpdatingSldier = true;
                
                Debug.Log("set magnitude: "+ magnitude);
            }
            else {
                controlledObject = null;
            }

        }
        else if (stringDetected.Equals(hideSliderCommand.ToLower()))
        {
            Debug.Log("hide slider command");
            if (Slider.activeSelf)
            {
                Slider.SetActive(false);
                GameObject controlledObject = null;
                sliderReactor = null;
                Debug.Log("slider hidden");
            }
        }
        
    }

    // Update is called once per frame
    void Update () {
        //if (isUpdatingSldier) {
        //    sliderReactor.setMagnitude(magnitude);
        //}

        if (Slider != null && Slider.activeSelf)
        {
            float magnitude = LegoControllerManager.Instance.stretchThis / 7f; 
            sliderControl.SetSliderValue(magnitude);
            sliderReactor.setMagnitude(magnitude);

            ArrowManager arrowManager = sliderReactor.child.GetComponent<ArrowManager>();
            // Send the value to other hololenses
            CustomMessages.Instance.SendForceFieldArrowUpdated(sliderReactor.gameObject.name, arrowManager.ArrowRotation, magnitude); 
        }
		
	}
}
