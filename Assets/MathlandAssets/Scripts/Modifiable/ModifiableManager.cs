using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableManager : MonoBehaviour
{

    public GameObject selectedObjectToModify;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Rotate

    public void rotate()
    {
        selectedObjectToModify.GetComponent<Rotate>().rotate();
    }

    public void rotateX()
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateX();
    }

    public void rotateY()
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateY();
    }

    public void rotateZ()
    {
        selectedObjectToModify.GetComponent<Rotate>().rotateZ();
    }

    #endregion

    #region resize

    public void resize()
    {
        selectedObjectToModify.GetComponent<Resize>().resize();
    }

    public void resizeX()
    {
        selectedObjectToModify.GetComponent<Resize>().resizeX();
    }

    public void resizeY()
    {
        selectedObjectToModify.GetComponent<Resize>().resizeY();
    }

    public void resizeZ()
    {
        selectedObjectToModify.GetComponent<Resize>().resizeZ();
    }

    #endregion

    #region Reposition

    public void reposition()
    {
        selectedObjectToModify.GetComponent<Reposition>().reposition();
    }

    public void repositionX()
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionX();
    }

    public void repositionY()
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionY();
    }

    public void repositionZ()
    {
        selectedObjectToModify.GetComponent<Reposition>().repositionZ();
    }

    #endregion
}
