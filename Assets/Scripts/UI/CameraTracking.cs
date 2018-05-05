using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    #region Attributes
    public Transform track; // The transform to track
	private float camHeight;
	private float camWidth;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
        Camera cam = GetComponent<Camera>();
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameController.instance.map != null)
        {
            if (track.position.x - camWidth / 2f > 0f && track.position.x + camWidth / 2f < GameController.instance.map.Width - 1f)
            {
                transform.position = new Vector3(track.position.x, transform.position.y, -10);
            }

            if (track.position.y + camHeight / 2f < 0f && track.position.y - camHeight / 2f > -GameController.instance.map.Height + 1f)
            {
                transform.position = new Vector3(transform.position.x, track.position.y, -10);
            }
        }
		//transform.position = new Vector3(track.position.x, track.position.y, - 10);
    }
    #endregion

	/*void updateMap(Map newMap)
	{
		mapHeight = newMap.Height;
		mapWidth = newMap.Width;
	}*/
}
