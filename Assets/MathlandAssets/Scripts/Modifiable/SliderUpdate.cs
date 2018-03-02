using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class SliderUpdate : MonoBehaviour {

	public int MAX_ROTATION_VALUE = 360; 
	public int MAX_REPOSITION_VALUE = 1000;

	[Tooltip("Does rotation require an object to be selected?")]
	public LeanSelectable RequiredSelectable;

	#if UNITY_EDITOR
	protected virtual void Reset()
	{
		Start();
	}
	#endif

	protected virtual void Start()
	{
		if (RequiredSelectable == null)
		{
			RequiredSelectable = GetComponent<LeanSelectable>();
		}
	}

	bool[] axisToModify = { true, true, true };
	
	// Update is called once per frame
	protected virtual void Update () {
		// If we require a selectable and it isn't selected, cancel rotation
		if (RequiredSelectable != null && RequiredSelectable.IsSelected == false)
		{
			return;
		}

		bool sliderValueChanged = ModifiableManager.Instance.sliderValueChanged;
		Debug.Log ("before change " + sliderValueChanged);

		if (!sliderValueChanged) {
			Debug.Log ("Slider value not changed");
			return;
		}
		ModifiableManager.ModifyingAction action = ModifiableManager.Instance.action;
		float value = ModifiableManager.Instance.getSliderValue ();
		axisToModify = ModifiableManager.Instance.axisToModify;
		if (action == ModifiableManager.ModifyingAction.REPOSITION) {
			repositionAction(value);
		} else if (action == ModifiableManager.ModifyingAction.RESIZE) {
			resizeAction(value);
		} else if (action == ModifiableManager.ModifyingAction.ROTATE) {
			Debug.Log ("Call rotateAction");
			rotateAction(value);
		}
		ModifiableManager.Instance.sliderValueChanged = false;
		Debug.Log ("after change " + sliderValueChanged);
		
	}

	#region Rotate
	void rotateAction (float value) {
		value *= MAX_ROTATION_VALUE;

		if (axisToModify[0] && axisToModify[1] && axisToModify[2]) rotate(value);
		if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { rotateX(value); rotateY(value); }
		if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { rotateX(value); rotateZ(value); }
		if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { rotateX(value); }
		if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { rotateY(value); rotateZ(value); }
		if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { rotateY(value); }
		if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { rotateZ(value); }
	}

	void rotate(float value)
	{
		Debug.Log("Rotating all");
		GetComponent<Rotate>().rotate(value);
	}

	void rotateX(float value)
	{
		Debug.Log("Rotating X");
		GetComponent<Rotate>().rotateX(value);
	}

	void rotateY(float value)
	{
		Debug.Log("Rotating Y");
		GetComponent<Rotate>().rotateY(value);
	}

	void rotateZ(float value)
	{
		Debug.Log("Rotating Z");
		GetComponent<Rotate>().rotateZ(value);
	}

	#endregion

	#region resize
	void resizeAction(float value)
	{
		value += 0.5f;

		if (axisToModify[0] && axisToModify[1] && axisToModify[2]) resize(value);
		if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { resizeX(value); resizeY(value); }
		if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { resizeX(value); resizeZ(value); }
		if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { resizeX(value); }
		if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { resizeY(value); resizeZ(value); }
		if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { resizeY(value); }
		if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { resizeZ(value); }
	}

	void resize(float value)
	{
		GetComponent<Resize>().resize(value);
	}

	void resizeX(float value)
	{
		GetComponent<Resize>().resizeX(value);
	}

	void resizeY(float value)
	{
		GetComponent<Resize>().resizeY(value);
	}

	void resizeZ(float value)
	{
		GetComponent<Resize>().resizeZ(value);
	}

	#endregion

	#region Reposition
	void repositionAction(float value)
	{
		value *= MAX_REPOSITION_VALUE;

		if (axisToModify[0] && axisToModify[1] && axisToModify[2]) reposition(value);
		if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { repositionX(value); repositionY(value); }
		if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { repositionX(value); repositionZ(value); }
		if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { repositionX(value); }
		if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { repositionY(value); repositionZ(value); }
		if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { repositionY(value); }
		if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { repositionZ(value); }
	}

	void reposition(float value)
	{
		GetComponent<Reposition>().reposition(value);
	}

	void repositionX(float value)
	{
		GetComponent<Reposition>().repositionX(value);
	}

	void repositionY(float value)
	{
		GetComponent<Reposition>().repositionY(value);
	}

	void repositionZ(float value)
	{
		GetComponent<Reposition>().repositionZ(value);
	}

	#endregion
}
