using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    const char DELIMITER = '_';
    const string SCENE = "scene";

    public GameObject forwardButton;
    public GameObject backButton;
    public GameObject sceneNameText;

    private int currentScene;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void getCurrentScene()
    {
        string name = SceneManager.GetActiveScene().name;
        currentScene = Convert.ToInt32(name.Split('_')[1]);
    }

    public void goForward() {
        string newScene = SCENE + (currentScene + 1).ToString();
        SceneManager.LoadScene(newScene);
    }

    public void goBack(){
        string newScene = SCENE + (currentScene - 1).ToString();
        SceneManager.LoadScene(newScene);
    }
}
