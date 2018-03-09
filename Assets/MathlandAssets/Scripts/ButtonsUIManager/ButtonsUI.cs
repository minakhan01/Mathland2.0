using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    [Header("Button groups")]
    public GameObject playUI;
    public GameObject buildUI;
    public GameObject editObjectUI;
	public GameObject ObjectsUI;
    public Slider slider;

    [Header("Axis")]
	// Buttons for Build Mode 
    public ButtonProperties XButton;
    public ButtonProperties YButton;
    public ButtonProperties ZButton;

    [Header("Build Mode")]
    public ButtonProperties MoveButton;
    public ButtonProperties ResizeButton;
    public ButtonProperties RotateButton;

    [Header("Add Mode")]
	// Buttons for Add Mode
    public ButtonProperties GraphButton;
    public ButtonProperties PlayButton;
    public ButtonProperties RewindButton;
    public ButtonProperties AddButton;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void switchAction (ModifiableManager.ModifyingAction action) {
        switch (action) {
            case ModifiableManager.ModifyingAction.REPOSITION:
                MoveButton.switchState();
                ResizeButton.cancelState();
                RotateButton.cancelState();
                break;
            case ModifiableManager.ModifyingAction.ROTATE:
                RotateButton.switchState();
                MoveButton.cancelState();
                ResizeButton.cancelState();
                break;
            case ModifiableManager.ModifyingAction.RESIZE:
                ResizeButton.switchState();
                MoveButton.cancelState();
                RotateButton.cancelState();
                break;
        }
    }

	//PLAY_SCREEN
    public void AddButtonHandler()
    {
        Debug.Log("Add Object");
        ObjectsUI.SetActive(!ObjectsUI.activeSelf);
        AddButton.switchState();
		ModifiableManager.Instance.playMenuSelected[0] = !ModifiableManager.Instance.playMenuSelected[0];
    }

    public void PlayButtonHandler()
    {
        Debug.Log("Play");
        PlayButton.switchState();
		ModifiableManager.Instance.playMenuSelected[1] = !ModifiableManager.Instance.playMenuSelected[1];
        //BallPhysicsManager.Instance.setInitialPosition();
        GameStateManager.Instance.currentPhysicsPlayState = GameStateManager.GamePlayPhysicsState.ON;

        if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
            //Has changed to on
            BallPhysicsManager.Instance.setInitialPosition();
        }else {
            //Has changed to off -> reset
            BallPhysicsManager.Instance.resetBallPosition();
        }
    }

    public void RewindButtonHandler()
    {
        Debug.Log("Rewind");
        RewindButton.switchState();
		ModifiableManager.Instance.playMenuSelected[2] = !ModifiableManager.Instance.playMenuSelected[2];

    }

    public void GraphButtonHandler()
    {
        Debug.Log("Graph");
        GraphButton.switchState();
		ModifiableManager.Instance.playMenuSelected[3] = !ModifiableManager.Instance.playMenuSelected[3];

    }

	public void AddForceButtonHandler()
	{
		GameToolManager.Instance.CreateForceField ();
		Debug.Log("Graph");
	}

	public void AddVelocityButtonHandler()
	{
		GameToolManager.Instance.CreateVelocityVector ();
		Debug.Log("Graph");
	}

	public void AddRopeButtonHandler()
	{
		GameToolManager.Instance.CreateRope ();
		Debug.Log("Graph");
	}

	public void AddCubeButtonHandler()
	{
		GameToolManager.Instance.CreateCube ();
		Debug.Log("Graph");
	}

	public void AddRampButtonHandler()
	{
		GameToolManager.Instance.CreateRamp ();
		Debug.Log("Ramp");
	}

	//MODIFY_SCREEN
    public void MoveButtonHandler()
    {
        switchAction(ModifiableManager.ModifyingAction.REPOSITION);
		ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.REPOSITION;
		ModifiableManager.Instance.actionSelected[0] = !ModifiableManager.Instance.actionSelected[0];
        Debug.Log("Move");
    }

    public void ResizeButtonHandler()
    {
        switchAction(ModifiableManager.ModifyingAction.RESIZE);
        ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.RESIZE;
		ModifiableManager.Instance.actionSelected[1] = !ModifiableManager.Instance.actionSelected[1];
        Debug.Log("Resize");
    }

    public void RotateButtonHandler()
    {
        switchAction(ModifiableManager.ModifyingAction.ROTATE);
		ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.ROTATE;
		ModifiableManager.Instance.actionSelected[2] = !ModifiableManager.Instance.actionSelected[2];
        Debug.Log("Rotate");
    }

    public void DeleteButtonHandler()
    {
		GameToolManager.Instance.DestroyObject();
        Debug.Log("Delete");
		GameStateManager.resetDisplayState ();
    }

	public void XAxisHandler()
	{
	
        XButton.switchState();
        ModifiableManager.Instance.axisToModify[0] = !ModifiableManager.Instance.axisToModify[0];
		Debug.Log("XAxisHandler");
	}

	public void YAxisHandler()
	{
        YButton.switchState();
        ModifiableManager.Instance.axisToModify[1] = !ModifiableManager.Instance.axisToModify[1];
		Debug.Log("YAxisHandler");
	}

	public void ZAxisHandler()
	{
        ZButton.switchState();
        ModifiableManager.Instance.axisToModify[2] = !ModifiableManager.Instance.axisToModify[2];
		Debug.Log("ZAxisHandler");
	}

	public void SliderHandler() {
		ModifiableManager.Instance.sliderValueChangeHandler (slider.value);
		Debug.Log ("Handle slider value");
	}

    public void resetBuildButtons () {
        XButton.cancelState();
        YButton.cancelState();
        ZButton.cancelState();
        MoveButton.cancelState();
        ResizeButton.cancelState();
        RotateButton.cancelState();
        slider.value = 0;
    }

}
