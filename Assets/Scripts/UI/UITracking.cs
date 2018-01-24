using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour {
    public GameObject obj;  // The object to track
    public Vector3 offset;  // The offset in screen pixels

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(obj.transform.position) + offset;
	}
}
