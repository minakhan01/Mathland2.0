using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class UIManager : Singleton<UIManager>
{
    public GameObject buttons; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Switches the UI from one mode to another (Play and build)
    public void switchUI()
    {
        if (GameStateManager.currentGameState == GameStateManager.gameState.PLAY)
        {
            //PLAY MODE
            hideBuildUI();
            showPlayUI();
        }
        else if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD)
        {
            //BUILD MODE
            hidePlayUI();
            resetBuildUI();
			showBuildUI ();
        } 
    }


    void showBuildUI()
    {
        buttons.GetComponent<ButtonsUI>().buildUI.SetActive(true);
    }

    void hideBuildUI()
    {
        buttons.GetComponent<ButtonsUI>().buildUI.SetActive(false);
    }
		
    void showPlayUI()
    {
        buttons.GetComponent<ButtonsUI>().playUI.SetActive(true);
    }

    void hideObjectsUI()
    {
        buttons.GetComponent<ButtonsUI>().ObjectsUI.SetActive(false);
    }

    void showObjectsUI()
    {
        buttons.GetComponent<ButtonsUI>().ObjectsUI.SetActive(true);
    }

    void hidePlayUI()
    {
        buttons.GetComponent<ButtonsUI>().playUI.SetActive(false);
    }

    void resetBuildUI()
    {
        buttons.GetComponent<ButtonsUI>().resetBuildButtons();
    }


}
