using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    public GameObject playUI;
    public GameObject buildUI;
    public GameObject editObjectUI;
	public GameObject ObjectsUI;
    public Slider slider;

	// Buttons for Build Mode 
	public Button XButton;
	public Button YButton;
	public Button ZButton;

	public Button MoveButton;
	public Button ResizeButton;
	public Button RotateButton;

	// Buttons for Add Mode
	public Button GraphButton;
	public Button PlayButton;
	public Button RewindButton;
	public Button AddButton;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

	//PLAY_SCREEN
    public void AddButtonHandler()
    {
        Debug.Log("Add Object");
		if (ModifiableManager.Instance.playMenuSelected[0]) {
			AddButton.targetGraphic.color = Color.black;
			GetComponent<ButtonsUI>().ObjectsUI.SetActive(true);
		} else {
			AddButton.targetGraphic.color = Color.white;
			GetComponent<ButtonsUI>().ObjectsUI.SetActive(false);
		}
		ModifiableManager.Instance.playMenuSelected[0] = !ModifiableManager.Instance.playMenuSelected[0];
    }

    public void PlayButtonHandler()
    {
        Debug.Log("Play");
		if (ModifiableManager.Instance.playMenuSelected[1]) {
			PlayButton.targetGraphic.color = Color.black;
		} else {
			PlayButton.targetGraphic.color = Color.white;
		}
		ModifiableManager.Instance.playMenuSelected[1] = !ModifiableManager.Instance.playMenuSelected[1];

    }

    public void RewindButtonHandler()
    {
        Debug.Log("Rewind");
		if (ModifiableManager.Instance.playMenuSelected[2]) {
			RewindButton.targetGraphic.color = Color.black;
		} else {
			RewindButton.targetGraphic.color = Color.white;
		}
		ModifiableManager.Instance.playMenuSelected[2] = !ModifiableManager.Instance.playMenuSelected[2];

    }

    public void GraphButtonHandler()
    {
        Debug.Log("Graph");
		if (ModifiableManager.Instance.playMenuSelected[3]) {
			GraphButton.targetGraphic.color = Color.black;
		} else {
			GraphButton.targetGraphic.color = Color.white;
		}
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
		ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.REPOSITION;
		if (ModifiableManager.Instance.actionSelected[0]) {
			MoveButton.targetGraphic.color = Color.black;
		} else {
			MoveButton.targetGraphic.color = Color.white;
		}
		ModifiableManager.Instance.actionSelected[0] = !ModifiableManager.Instance.actionSelected[0];
        Debug.Log("Move");
    }

    public void ResizeButtonHandler()
    {
        ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.RESIZE;
		if (ModifiableManager.Instance.actionSelected[1]) {
			ResizeButton.targetGraphic.color = Color.black;
		} else {
			ResizeButton.targetGraphic.color = Color.white;
		}
		ModifiableManager.Instance.actionSelected[1] = !ModifiableManager.Instance.actionSelected[1];
        Debug.Log("Resize");
    }

    public void RotateButtonHandler()
    {
		ModifiableManager.Instance.action = ModifiableManager.ModifyingAction.ROTATE;
		if (ModifiableManager.Instance.actionSelected[2]) {
			RotateButton.targetGraphic.color = Color.black;
		} else {
			RotateButton.targetGraphic.color = Color.white;
		}
		ModifiableManager.Instance.actionSelected[2] = !ModifiableManager.Instance.actionSelected[2];
        Debug.Log("Rotate");
    }

    public void DeleteButtonHandler()
    {
		GameToolManager.Instance.DestroyAllGameTools ();
        Debug.Log("Delete");
    }

	public void XAxisHandler()
	{
		if (ModifiableManager.Instance.axisToModify [0]) {
			Debug.Log ("Change X button to black");
			XButton.targetGraphic.color = Color.black;
		} else {
			Debug.Log ("Change X button to white");
			XButton.targetGraphic.color = Color.white;
		}
        ModifiableManager.Instance.axisToModify[0] = !ModifiableManager.Instance.axisToModify[0];
		Debug.Log("XAxisHandler");
	}

	public void YAxisHandler()
	{
		if (ModifiableManager.Instance.axisToModify [1]) {
			Debug.Log ("Change Y button to black");
			YButton.targetGraphic.color = Color.black;
		} else {
			Debug.Log ("Change Y button to white");
			YButton.targetGraphic.color = Color.white;
		}
        ModifiableManager.Instance.axisToModify[1] = !ModifiableManager.Instance.axisToModify[1];
		Debug.Log("YAxisHandler");
	}

	public void ZAxisHandler()
	{
		if (ModifiableManager.Instance.axisToModify [2]) {
			Debug.Log ("Change Z button to black");
			ZButton.targetGraphic.color = Color.black;
		} else {
			Debug.Log ("Change Z button to white");
			ZButton.targetGraphic.color = Color.white;
		}
        ModifiableManager.Instance.axisToModify[2] = !ModifiableManager.Instance.axisToModify[2];
		Debug.Log("ZAxisHandler");
	}

	public void SliderHandler() {
		ModifiableManager.Instance.sliderValueChangeHandler (slider.value);
		Debug.Log ("Handle slider value");
	}
}
