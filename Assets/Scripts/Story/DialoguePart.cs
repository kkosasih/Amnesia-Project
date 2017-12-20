using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePart {
    protected float time1;
    protected float time2;

    // Constructor taking in the time it takes to perform the action
    public DialoguePart (float t1, float t2)
    {
        time1 = t1;
        time2 = t2;
    }

    // Wait for time1, do something, then wait for time2
    public virtual IEnumerator PerformPart ()
    {
        yield return new WaitForSeconds(time1);
        yield return new WaitForSeconds(time2);
    }
}
