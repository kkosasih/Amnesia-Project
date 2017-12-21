using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statement : DialoguePart {
    private string speaker;
    private Sprite portrait;
    private string speech;

    // Constructor taking in the time it takes to perform the action
    public Statement (float t1, float t2, string sr, Sprite p, string sh) : base(t1, t2)
    {
        speaker = sr;
        portrait = p;
        speech = sh;
    }

    // Constructor taking in a string
    public Statement (string data) : base(data)
    {
        string[] parameters = data.Split('|');
        speaker = parameters[2];
        portrait = Resources.Load<Sprite>("Character Portraits/" + speaker + "/" + parameters[3]);
        speech = parameters[4];
    }

    // Wait for time1, say the dialogue, then wait for time2 after the player clicks "next"
    public override IEnumerator PerformPart ()
    {
        isRunning = true;
        GameObject box = GameObject.Find("DialogueBox");
        yield return new WaitForSeconds(time1);
        box.GetComponent<UIPanel>().isOpen = true;
        box.transform.Find("Portrait").gameObject.GetComponent<Image>().sprite = portrait;
        box.transform.Find("Speaker").gameObject.GetComponent<Text>().text = speaker;
        box.transform.Find("Speech").gameObject.GetComponent<Text>().text = speech;
        while (box.GetComponent<UIPanel>().isOpen)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
