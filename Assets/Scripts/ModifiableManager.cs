using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;


public class ModifiableManager : Singleton<ModifiableManager>
{
    public int MAX_ROTATION_VALUE = 360; 
    public int MAX_REPOSITION_VALUE = 1000;

    public enum ModifyingAction { ROTATE, RESIZE, REPOSITION };
    public bool[] axisToModify = { true, true, true }; //{X, Y, Z}
    public bool[] actionSelected = { true, true, true }; //{REPOSITION, RESIZE, ROTATE}
    public bool[] playMenuSelected = { true, true, true, true }; //{ADD, PLAY, REWIND, DELETE}
    public ModifyingAction action { get; set; }
    public GameObject selectedObjectToModify;

	public bool sliderValueChanged = false;
	float sliderValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

	public float getSliderValue() {
		return sliderValue;
	}

	public bool sliderValueChange() {
		return sliderValueChanged;
	}

    public void sliderValueChangeHandler(float value)
    {
		sliderValue = value;
		sliderValueChanged = true;
    }

//    #region Rotate
//    void rotateAction (float value) {
//
//        value *= MAX_ROTATION_VALUE;
//
//        if (axisToModify[0] && axisToModify[1] && axisToModify[2]) rotate(value);
//        if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { rotateX(value); rotateY(value); }
//        if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { rotateX(value); rotateZ(value); }
//        if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { rotateX(value); }
//        if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { rotateY(value); rotateZ(value); }
//        if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { rotateY(value); }
//        if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { rotateZ(value); }
//    }
//
//    void rotate(float value)
//    {
//        Debug.Log("Rotating all");
//        selectedObjectToModify.GetComponent<Rotate>().rotate(selectedObjectToModify, value);
//    }
//
//    void rotateX(float value)
//    {
//        Debug.Log("Rotating X");
//        selectedObjectToModify.GetComponent<Rotate>().rotateX(selectedObjectToModify, value);
//    }
//
//    void rotateY(float value)
//    {
//        Debug.Log("Rotating Y");
//        selectedObjectToModify.GetComponent<Rotate>().rotateY(selectedObjectToModify, value);
//    }
//
//    void rotateZ(float value)
//    {
//        Debug.Log("Rotating Z");
//        selectedObjectToModify.GetComponent<Rotate>().rotateZ(selectedObjectToModify, value);
//    }
//
//    #endregion
//
//    #region resize
//    void resizeAction(float value)
//    {
//        value += 0.5f;
//
//        if (axisToModify[0] && axisToModify[1] && axisToModify[2]) resize(value);
//        if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { resizeX(value); resizeY(value); }
//        if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { resizeX(value); resizeZ(value); }
//        if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { resizeX(value); }
//        if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { resizeY(value); resizeZ(value); }
//        if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { resizeY(value); }
//        if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { resizeZ(value); }
//    }
//
//    void resize(float value)
//    {
//        selectedObjectToModify.GetComponent<Resize>().resize(selectedObjectToModify, value);
//    }
//
//    void resizeX(float value)
//    {
//        selectedObjectToModify.GetComponent<Resize>().resizeX(selectedObjectToModify, value);
//    }
//
//    void resizeY(float value)
//    {
//        selectedObjectToModify.GetComponent<Resize>().resizeY(selectedObjectToModify, value);
//    }
//
//    void resizeZ(float value)
//    {
//        selectedObjectToModify.GetComponent<Resize>().resizeZ(selectedObjectToModify, value);
//    }
//
//    #endregion
//
//    #region Reposition
//    void repositionAction(float value)
//    {
//        value *= MAX_REPOSITION_VALUE;
//
//        if (axisToModify[0] && axisToModify[1] && axisToModify[2]) reposition(value);
//        if (axisToModify[0] && axisToModify[1] && !axisToModify[2]) { repositionX(value); repositionY(value); }
//        if (axisToModify[0] && !axisToModify[1] && axisToModify[2]) { repositionX(value); repositionZ(value); }
//        if (axisToModify[0] && !axisToModify[1] && !axisToModify[2]) { repositionX(value); }
//        if (!axisToModify[0] && axisToModify[1] && axisToModify[2]) { repositionY(value); repositionZ(value); }
//        if (!axisToModify[0] && axisToModify[1] && !axisToModify[2]) { repositionY(value); }
//        if (!axisToModify[0] && !axisToModify[1] && axisToModify[2]) { repositionZ(value); }
//    }
//
//    void reposition(float value)
//    {
//        selectedObjectToModify.GetComponent<Reposition>().reposition(selectedObjectToModify, value);
//    }
//
//    void repositionX(float value)
//    {
//        selectedObjectToModify.GetComponent<Reposition>().repositionX(selectedObjectToModify, value);
//    }
//
//    void repositionY(float value)
//    {
//        selectedObjectToModify.GetComponent<Reposition>().repositionY(selectedObjectToModify, value);
//    }
//
//    void repositionZ(float value)
//    {
//        selectedObjectToModify.GetComponent<Reposition>().repositionZ(selectedObjectToModify, value);
//    }
//
//    #endregion

}
