using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : DialoguePart {
    public char optionChosen = 'A';                     // The option that was selected from choices
    private List<string> options = new List<string>();  // The text of the given options

    // Change the variables based on a string
    public override void ChangeSettings (string data)
    {
        base.ChangeSettings(data);
        string[] parameters = data.Split('|');
        for (int i = 2; i < parameters.Length; ++i)
        {
            options.Add(parameters[i]);
        }
    }

    // Changes the option chosen
    public void ChooseOption (char option)
    {
        optionChosen = option;
    }

    // Wait for time1, say the dialogue, then wait for time2 after the player clicks "next"
    public override IEnumerator PerformPart ()
    {
        isRunning = true;
        GameObject box = GameObject.Find("ChoiceBox");
        yield return new WaitForSeconds(time1);
        box.GetComponent<UIPanel>().isOpen = true;
        box.GetComponent<TextOptions>().CreateOptions(options);
        while (box.GetComponent<UIPanel>().isOpen)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}