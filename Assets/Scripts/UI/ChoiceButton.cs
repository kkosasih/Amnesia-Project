using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {
    public char choice = 'A';

    // Change the dialogueController option
    public void SetChoice ()
    {
        DialogueController.instance.branch = choice;
    }
}
