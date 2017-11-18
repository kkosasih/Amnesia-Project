using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour {
    public GameObject obj;
    public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = cam.WorldToScreenPoint(obj.transform.position) + new Vector3(0, 40, 0);
	}
}
