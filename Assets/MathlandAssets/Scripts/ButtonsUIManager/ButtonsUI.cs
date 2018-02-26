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


    #region build callbacks

    public void addObject()
    {
        Debug.Log("Add Object");
    }

    public void play()
    {
        Debug.Log("Play");
    }

    #endregion

    #region play callbacks

    public void rewind()
    {
        Debug.Log("Rewind");
    }

    public void graph()
    {
        Debug.Log("Graph");
    }

    public void build()
    {
        Debug.Log("Build");
    }

    #endregion

    #region edit Object callbacks

    public void move()
    {
        Debug.Log("Move");
    }

    public void resize()
    {
        Debug.Log("Resize");
    }

    public void rotate()
    {
        Debug.Log("Rotate");
    }

    public void delete()
    {
        Debug.Log("Delete");
    }
    #endregion
}
