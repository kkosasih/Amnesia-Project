using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePart : MonoBehaviour {
    #region Attributes
    public bool isRunning = false;  // Whether the part is active
    protected float time1;          // The time to take before starting the part
    protected float time2;          // The time to take after ending the part
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods
    // Change the variables based on a string
    public virtual void ChangeSettings (string data)
    {
        string[] parameters = data.Split('|');
        time1 = float.Parse(parameters[0]);
        time2 = float.Parse(parameters[1]);
    }
    #endregion

    #region Coroutines
    // Wait for time1, do something, then wait for time2
    public virtual IEnumerator PerformPart ()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
    #endregion
}
