using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    #region Attributes
    public Transform track; // The transform to track
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(track.position.x, track.position.y, - 10);
    }
    #endregion
}
