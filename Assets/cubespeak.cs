using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;
using System;
using HoloToolkit.Unity.InputModule;

public class cubespeak : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TextToSpeechManager tts = GetComponent<TextToSpeechManager>();
        tts.Voice = TextToSpeechVoice.Mark;
        tts.SpeakText("Everything is good to go.");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
