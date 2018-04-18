using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class UIManager : Singleton<UIManager>
{
    public GameObject buttons;
    public GameObject rewindUI;
    public GameObject playUI;
    public GameObject buildUI;
    public GameObject startSimulationUI;
    public GameObject graphUI;
    public GameObject linesUI;
    public GameObject linesLegendUI;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Switches the UI from one mode to another (Play and build)
    public void switchScreenUI()
    {
        if (GameStateManager.currentGameState == GameStateManager.gameState.PLAY)
        {
            //PLAY MODE
            hideAllUI();
            showGraphUI();
            showPlayUI();
        }
        else if (GameStateManager.currentGameState == GameStateManager.gameState.BUILD)
        {
            //BUILD MODE
            RewindManager.Instance.Reset();
            hideAllUI();
            hideGraphsUI();
            resetBuildUIButtons();
            showBuildUI();
        }
        else if (GameStateManager.currentGameState == GameStateManager.gameState.REWIND)
        {
            //REWIND MODE
            hideAllUI();
            showGraphUI();
            showRewindUI();
        }
    }

    public void hideGraphsUI()
    {
        graphUI.SetActive(false);
        linesUI.SetActive(false);
        linesLegendUI.SetActive(false);
    }

    public void showGraphUI()
    {
        graphUI.SetActive(true);
        linesUI.SetActive(true);
        linesLegendUI.SetActive(true);
    }

    void hideAllUI()
    {
        buildUI.SetActive(false);
        playUI.SetActive(false);
        rewindUI.SetActive(false);
        hideGraphsUI();
    }


    void showBuildUI()
    {
        buildUI.SetActive(true);
    }

    //void hideBuildUI()
    //{
    //    buttons.GetComponent<ButtonsUI>().buildUI.SetActive(false);
    //}

    //void showGraphUI()
    //{
    //	buttons.GetComponent<ButtonsUI>().GraphUI.SetActive(true);
    //}

    //void hideGraphUI()
    //{
    //	buttons.GetComponent<ButtonsUI>().GraphUI.SetActive(false);
    //}

    void showRewindUI()
    {
        rewindUI.SetActive(true);
    }

    //void hideRewindUI()
    //{
    //	buttons.GetComponent<ButtonsUI>().RewindUI.SetActive(false);
    //}

    //void showStartSimulationUI()
    //{
    //	buttons.GetComponent<ButtonsUI>().StartSimulationUI.SetActive(true);
    //}

    //void hideStartSimulationUI()
    //{
    //	buttons.GetComponent<ButtonsUI>().StartSimulationUI.SetActive(false);
    //}

    void showPlayUI()
    {
        playUI.SetActive(true);
    }

    //void hideObjectsUI()
    //{
    //    buttons.GetComponent<ButtonsUI>().ObjectsUI.SetActive(false);
    //}

    //void showObjectsUI()
    //{
    //    buttons.GetComponent<ButtonsUI>().ObjectsUI.SetActive(true);
    //}

    //void hidePlayUI()
    //{
    //    buttons.GetComponent<ButtonsUI>().playUI.SetActive(false);
    //}

    void resetBuildUIButtons()
    {
        buttons.GetComponent<ButtonsUI>().resetBuildButtons();
    }


}
