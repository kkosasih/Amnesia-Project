using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {
    public char choice = 'A';   // The choice that is accompanied with the button

    // Change the dialogueController option
    public void SetChoice ()
    {
        DialogueController.instance.branch = choice;
        GameObject.Find("ChoiceBox").GetComponent<UIPanel>().isOpen = false;
    }
}
