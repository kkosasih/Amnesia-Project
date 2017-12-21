using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePart : MonoBehaviour {
    public bool isRunning = false;
    protected float time1;
    protected float time2;

    // Change the variables based on a string
    public virtual void ChangeSettings (string data)
    {
        string[] parameters = data.Split('|');
        time1 = float.Parse(parameters[0]);
        time2 = float.Parse(parameters[1]);
    }

    // Wait for time1, do something, then wait for time2
    public virtual IEnumerator PerformPart ()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
