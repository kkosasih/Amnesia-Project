using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    // Moves the camera to the player
    public void UpdatePos (Vector3 newPos)
    {
        transform.position = newPos;
    }
}
