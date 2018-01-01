using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMask : DialoguePart {
    private float time3;
    private Color color;

    // Change the variables based on a string
    public override void ChangeSettings (string data)
    {
        base.ChangeSettings(data);
        string[] parameters = data.Split('|');
        time3 = float.Parse(parameters[2]);
        string[] colorPoints = parameters[3].Split(',');
        color = new Color(float.Parse(colorPoints[0]), float.Parse(colorPoints[1]), float.Parse(colorPoints[2]), float.Parse(colorPoints[3]));
    }

    // Wait for time1, move the camera for time3 seconds, then wait for time2
    public override IEnumerator PerformPart ()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        yield return StartCoroutine(Helper.ChangeColorInTime(GameObject.FindWithTag("UIMask").GetComponent<Image>(), color, time3));
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
