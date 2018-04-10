using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    const char DELIMITER = '_';
    const string SCENE = "Scene";
    const int NUM_OF_SCENES = 10;

    public GameObject forwardButton;
    public GameObject backButton;
    public Text sceneNameText;

    int currentScene;

    // Use this for initialization
    void Start()
    {
        
        Debug.Log("Initiallize scene");
        currentScene = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
        GameStateManager.Instance.scene = currentScene;
        Debug.Log("currentScene: " + currentScene);
        sceneNameText.text = "Scene" + " " + currentScene.ToString();

        setCurrentScene();
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
        if (currentScene + 1 > NUM_OF_SCENES)
        {
            forwardButton.SetActive(false);
        }
        else if (currentScene - 1 < 1)
        {
            backButton.SetActive(false);
        }
    }

    public void goForward()
    {
		currentScene = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
		Debug.Log ("SwitchSceneManager: Going forward currentScene: "+currentScene);
        string newScene = SCENE + DELIMITER + (currentScene + 1).ToString();
        Debug.Log("SwitchSceneManager: Going forward newScene: " + newScene + ".");
        SceneManager.LoadScene(newScene);
    }

    public void goBack()
    {
		currentScene = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
        string newScene = SCENE + DELIMITER + (currentScene - 1).ToString();
        SceneManager.LoadScene(newScene);
		Debug.Log ("SwitchSceneManager: Going backward");
        Debug.Log("SwitchSceneManager: Going forward newScene: " + newScene + ".");
    }
}
