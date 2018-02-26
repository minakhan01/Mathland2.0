using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
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
            hideEditObjectUI();
            hideBuildUI();
            showPlayUI();
        }
        else if (GameStateManager.currentGameState == GameStateManager.gameState.TOOL_MENU)
        {
            //EDIT MODE
            hidePlayUI();
            hideBuildUI();
            showEditObjectUI();
        } else
        {
            //BUILD MODE
            hideEditObjectUI();
            hidePlayUI();
            showBuildUI();
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

    void hidePlayUI()
    {
        buttons.GetComponent<ButtonsUI>().playUI.SetActive(false);
    }

    void showEditObjectUI()
    {
        buttons.GetComponent<ButtonsUI>().editObjectUI.SetActive(true);
    }

    void hideEditObjectUI()
    {
        buttons.GetComponent<ButtonsUI>().editObjectUI.SetActive(false);
    }

}
