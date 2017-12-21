using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : DialoguePart {
    private float time3;
    private Vector3 moveTo;

    // Constructor taking in the time it takes to perform the action
    public MoveCam (float t1, float t2, float t3, Vector3 mt) : base(t1, t2)
    {
        time3 = t3;
        moveTo = mt;
    }

    // Constructor taking in a string
    public MoveCam (string data) : base(data)
    {
        string[] parameters = data.Split('|');
        time3 = float.Parse(parameters[2]);
        string[] vectorPoints = parameters[3].Split(',');
        moveTo = new Vector3(float.Parse(vectorPoints[0]), float.Parse(vectorPoints[1]), float.Parse(vectorPoints[2]));
    }

    // Wait for time1, move the camera for time3 seconds, then wait for time2
    public override IEnumerator PerformPart ()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        Helper.LerpMovement(GameObject.FindWithTag("MainCamera"), GameObject.FindWithTag("MainCamera").transform.position + moveTo, time3);
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
