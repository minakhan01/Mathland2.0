using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonProperties : MonoBehaviour
{
    Color selectedColor = Color.white;
    Color unselectedColor = Color.gray;

    bool selected;
    Button button;

    // Use this for initialization
    void Start()
    {
        selected = false;
        button = this.gameObject.GetComponent<Button>();

        setUIState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setUIState()
    {

        ColorBlock cb = button.colors;

        if (selected) button.targetGraphic.color = selectedColor;
        else button.targetGraphic.color = unselectedColor;

        button.colors = cb;
    }

    public void switchState()
    {
        selected = !selected;
        setUIState();
    }

    public void cancelState () {
        if (selected) switchState();
    }

}
