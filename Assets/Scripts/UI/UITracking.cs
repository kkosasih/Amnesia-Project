using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour {
    public GameObject obj;
    public Vector3 offset;

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
