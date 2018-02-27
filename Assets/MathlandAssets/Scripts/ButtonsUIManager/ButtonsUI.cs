using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUI : MonoBehaviour
{

    public GameObject settingsBtn;
    public GameObject playUI;
    public GameObject buildUI;
    public GameObject editObjectUI;

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
    }

    public void PlayButtonHandler()
    {
        Debug.Log("Play");
    }

    public void RewindButtonHandler()
    {
        Debug.Log("Rewind");
    }

    public void GraphButtonHandler()
    {
        Debug.Log("Graph");
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
        Debug.Log("Move");
    }

    public void ResizeButtonHandler()
    {
        Debug.Log("Resize");
    }

    public void RotateButtonHandler()
    {
        Debug.Log("Rotate");
    }

    public void DeleteButtonHandler()
    {
        Debug.Log("Delete");
    }

	public void XAxisHandler()
	{
		Debug.Log("XAxisHandler");
	}

	public void YAxisHandler()
	{
		Debug.Log("YAxisHandler");
	}

	public void ZAxisHandler()
	{
		Debug.Log("ZAxisHandler");
	}

	public void SliderHandler() {
		Debug.Log("Handle slider value");
	}
}
