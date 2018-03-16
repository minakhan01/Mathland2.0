using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class TouchTest : MonoBehaviour {

	private TapGestureRecognizer tapGesture;
	private TapGestureRecognizer doubleTapGesture;
	private TapGestureRecognizer tripleTapGesture;
	private SwipeGestureRecognizer swipeGesture;
	private PanGestureRecognizer panGesture;
	private ScaleGestureRecognizer scaleGesture;
	private RotateGestureRecognizer rotateGesture;
	private LongPressGestureRecognizer longPressGesture;

	private float nextAsteroid = float.MinValue;
	private GameObject draggingAsteroid;

	private readonly List<Vector3> swipeLines = new List<Vector3>();

	private void DebugText(string text, params object[] format)
	{
		//bottomLabel.text = string.Format(text, format);
		Debug.Log(string.Format(text, format));
	}
		

	private void HandleSwipe(float endX, float endY)
	{
		Debug.Log ("HandleSwipe");
	}

	private void TapGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Ended)
		{
			Debug.Log ("TapGesture");
		}
	}

	private void CreateTapGesture()
	{
		tapGesture = new TapGestureRecognizer();
		tapGesture.StateUpdated += TapGestureCallback;
		tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;
		FingersScript.Instance.AddGesture(tapGesture);
	}

	private void DoubleTapGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Ended)
		{
			Debug.Log ("DoubleTapEnded");
		}
	}

	private void CreateDoubleTapGesture()
	{
		doubleTapGesture = new TapGestureRecognizer();
		doubleTapGesture.NumberOfTapsRequired = 2;
		doubleTapGesture.StateUpdated += DoubleTapGestureCallback;
		doubleTapGesture.RequireGestureRecognizerToFail = tripleTapGesture;
		FingersScript.Instance.AddGesture(doubleTapGesture);
	}

	private void SwipeGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Ended)
		{
			Debug.Log ("SwipeGestureCallback");
		}
	}

	private void CreateSwipeGesture()
	{
		swipeGesture = new SwipeGestureRecognizer();
		swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
		swipeGesture.StateUpdated += SwipeGestureCallback;
		swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
		FingersScript.Instance.AddGesture(swipeGesture);
	}

	private void PanGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Executing)
		{
			DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);

			float deltaX = panGesture.DeltaX / 25.0f;
			float deltaY = panGesture.DeltaY / 25.0f;
			Debug.Log ("PanGestureCallback");
		}
	}

	private void CreatePanGesture()
	{
		panGesture = new PanGestureRecognizer();
		panGesture.MinimumNumberOfTouchesToTrack = 2;
		panGesture.StateUpdated += PanGestureCallback;
		FingersScript.Instance.AddGesture(panGesture);
	}

	private void ScaleGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Executing) {
			Debug.Log ("ScaleGestureCallback:"+scaleGesture.ScaleMultiplier);
		}
	}

	private void CreateScaleGesture()
	{
		scaleGesture = new ScaleGestureRecognizer();
		scaleGesture.StateUpdated += ScaleGestureCallback;
//		scaleGesture.ThresholdUnits
		FingersScript.Instance.AddGesture(scaleGesture);
	}

	private void RotateGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Executing)
		{
			Debug.Log ("RotateGestureCallback " + rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg);
		}
	}

	private void CreateRotateGesture()
	{
		rotateGesture = new RotateGestureRecognizer();
		rotateGesture.StateUpdated += RotateGestureCallback;
		rotateGesture.AngleThreshold = 0.1f;
		FingersScript.Instance.AddGesture(rotateGesture);
	}

	private void LongPressGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Began)
		{
			Debug.Log ("LongPressGestureCallback hello began");
		}
		else if (gesture.State == GestureRecognizerState.Executing)
		{
			Debug.Log ("LongPressGestureCallback hello executing");
		}
		else if (gesture.State == GestureRecognizerState.Ended)
		{
			Debug.Log ("LongPressGestureCallback hello ended");
		}
	}

	private void CreateLongPressGesture()
	{
		longPressGesture = new LongPressGestureRecognizer();
		longPressGesture.MaximumNumberOfTouchesToTrack = 1;
		longPressGesture.StateUpdated += LongPressGestureCallback;
		FingersScript.Instance.AddGesture(longPressGesture);
	}

	private void PlatformSpecificViewTapUpdated(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Ended)
		{
			Debug.Log("You triple tapped the platform specific label!");
		}
	}

	private void CreatePlatformSpecificViewTripleTapGesture()
	{
		tripleTapGesture = new TapGestureRecognizer();
		tripleTapGesture.StateUpdated += PlatformSpecificViewTapUpdated;
		tripleTapGesture.NumberOfTapsRequired = 3;
		FingersScript.Instance.AddGesture(tripleTapGesture);
	}

	private static bool? CaptureGestureHandler(GameObject obj)
	{
		// I've named objects PassThrough* if the gesture should pass through and NoPass* if the gesture should be gobbled up, everything else gets default behavior
		if (obj.name.StartsWith("PassThrough"))
		{
			// allow the pass through for any element named "PassThrough*"
			return false;
		}
		else if (obj.name.StartsWith("NoPass"))
		{
			// prevent the gesture from passing through, this is done on some of the buttons and the bottom text so that only
			// the triple tap gesture can tap on it
			return true;
		}

		// fall-back to default behavior for anything else
		return null;
	}

	// Use this for initialization
	void Start () {
		// don't reorder the creation of these :)
		CreatePlatformSpecificViewTripleTapGesture();
		CreateDoubleTapGesture();
		CreateTapGesture();
		CreateSwipeGesture();
		CreatePanGesture();
		CreateScaleGesture();
		CreateRotateGesture();
		CreateLongPressGesture();

		// pan, scale and rotate can all happen simultaneously
//		panGesture.AllowSimultaneousExecution(scaleGesture);
//		panGesture.AllowSimultaneousExecution(rotateGesture);
		scaleGesture.AllowSimultaneousExecution(rotateGesture);
//		scaleGesture.DisallowSimultaneousExecution(rotateGesture);
		// prevent the one special no-pass button from passing through,
		//  even though the parent scroll view allows pass through (see FingerScript.PassThroughObjects)
		FingersScript.Instance.CaptureGestureHandler = CaptureGestureHandler;

		// show touches, only do this for debugging as it can interfere with other canvases
		FingersScript.Instance.ShowTouches = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate()
	{
		
		int touchCount = Input.touchCount;
		if (FingersScript.Instance.TreatMousePointerAsFinger && Input.mousePresent)
		{
			touchCount += (Input.GetMouseButton(0) ? 1 : 0);
			touchCount += (Input.GetMouseButton(1) ? 1 : 0);
			touchCount += (Input.GetMouseButton(2) ? 1 : 0);
		}
		string touchIds = string.Empty;
		foreach (Touch t in Input.touches)
		{
			touchIds += ":" + t.fingerId + ":";
		}

	}
}
