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
        Image mask = GameObject.FindWithTag("UIMask").GetComponent<Image>();
        yield return new WaitForSeconds(time1);
        Color oldColor = mask.color;
        for (float timePassed = 0; timePassed < time3; timePassed += Time.deltaTime)
        {
            mask.color = Color.Lerp(oldColor, color, timePassed / time3);
            yield return new WaitForEndOfFrame();
        }
        mask.color = color;
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
