﻿using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;


public class ModifiableManager : Singleton<ModifiableManager>
{
    const int MAX_ROTATION_VALUE = 360;
    const int MAX_REPOSITION_VALUE = 1000;

    public Slider slider;

    public enum ModifyingAction { ROTATE, RESIZE, REPOSITION };
    public bool[] axisToModify = { true, true, true };
    public ModifyingAction action { get; set; }
    public GameObject selectedObjectToModify;

    // Use this for initialization
    void Start()
    {
        //slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void sliderValueChangeHandler(float value)
    {
        Debug.Log("AXIS TO MODIFY" + axisToModify[0].ToString() + axisToModify[1].ToString() + axisToModify[2].ToString());
        if (action == ModifyingAction.REPOSITION) {
            repositionAction(value);
        } else if (action == ModifyingAction.RESIZE) {
            resizeAction(value);
        } else if (action == ModifyingAction.ROTATE) {
            rotateAction(value);
        }

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
        selectedObjectToModify.GetComponent<Rotate>().rotate(selectedObjectToModify, value);
    }

    void rotateX(float value)
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateX(selectedObjectToModify, value);
    }

    void rotateY(float value)
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateY(selectedObjectToModify, value);
    }

    void rotateZ(float value)
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateZ(selectedObjectToModify, value);
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
        selectedObjectToModify.GetComponent<Resize>().resize(selectedObjectToModify, value);
    }

    void resizeX(float value)
    {
        selectedObjectToModify.GetComponent<Resize>().resizeX(selectedObjectToModify, value);
    }

    void resizeY(float value)
    {
        selectedObjectToModify.GetComponent<Resize>().resizeY(selectedObjectToModify, value);
    }

    void resizeZ(float value)
    {
        selectedObjectToModify.GetComponent<Resize>().resizeZ(selectedObjectToModify, value);
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
        selectedObjectToModify.GetComponent<Reposition>().reposition(selectedObjectToModify, value);
    }

    void repositionX(float value)
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionX(selectedObjectToModify, value);
    }

    void repositionY(float value)
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionY(selectedObjectToModify, value);
    }

    void repositionZ(float value)
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionZ(selectedObjectToModify, value);
    }

    #endregion

}
