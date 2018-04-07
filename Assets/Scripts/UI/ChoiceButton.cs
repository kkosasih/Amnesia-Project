using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {
    #region Attributes
    public char choice = 'A';   // The choice that is accompanied with the button
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods
    // Change the dialogueController option
    public void SetChoice ()
    {
        DialogueController.instance.Branch = choice;
        GameObject.Find("ChoiceBox").GetComponent<UIPanel>().IsOpen = false;
    }
    #endregion
}
