using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour {
    #region Attributes
    public GameObject obj;  // The object to track
    public Vector3 offset;  // The offset in screen pixels
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(obj.transform.position) + offset;
	}
    #endregion
}
