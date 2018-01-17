using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : DialoguePart {
    public char optionChosen = 'A';
    private List<string> options;

    // Change the variables based on a string
    public override void ChangeSettings (string data)
    {
        base.ChangeSettings(data);
        string[] parameters = data.Split('|');
        options.AddRange(parameters[5].Split(','));
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
        DialogueController.instance.branch = optionChosen;
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}