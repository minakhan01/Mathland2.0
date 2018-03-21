using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    const char DELIMITER = '_';
    const string SCENE = "scene";
    const int NUM_OF_SCENES = 10;

    public GameObject forwardButton;
    public GameObject backButton;
    public Text sceneNameText;

    private int currentScene;

    // Use this for initialization
    void Start()
    {
        currentScene = Convert.ToInt32(name.Split('_')[1]);
        Debug.Log("currentScene: " + currentScene);
        sceneNameText.text = currentScene.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    int getCurrentScene()
    {
        return currentScene;
    }

    void setCurrentScene()
    {
        if (currentScene == NUM_OF_SCENES)
        {
            forwardButton.SetActive(false);
        }
        else if (currentScene == 1)
        {
            backButton.SetActive(false);
        }
    }

    public void goForward()
    {
        string newScene = SCENE + DELIMITER + (currentScene + 1).ToString();
        SceneManager.LoadScene(newScene);
    }

    public void goBack()
    {
        string newScene = SCENE + DELIMITER + (currentScene - 1).ToString();
        SceneManager.LoadScene(newScene);
    }
}
