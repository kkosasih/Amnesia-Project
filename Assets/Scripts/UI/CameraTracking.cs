using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    #region Attributes
    public Transform track; // The transform to track
	private float mapHeight;
	private float mapWidth;
	private float camHeight;
	private float camWidth;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
		Camera cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		mapHeight = GameObject.Find ("ZotIsland").GetComponent<Map> ().Height;
		mapWidth = GameObject.Find ("ZotIsland").GetComponent<Map> ().Width;
		transform.position = new Vector3(track.position.x, track.position.y, - 10);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (track.position.x - camWidth / 2f > 0f && track.position.x + camWidth / 2f < mapWidth - 1f)
		{
			transform.position = new Vector3 (track.position.x, transform.position.y, -10);
		}

		if (track.position.y + camHeight / 2f < 0f && track.position.y - camHeight / 2f > -mapHeight + 1f)
		{
			transform.position = new Vector3 (transform.position.x, track.position.y, -10);
		}

		//transform.position = new Vector3(track.position.x, track.position.y, - 10);
    }
    #endregion

	void updateMap(Map newMap)
	{
		mapHeight = newMap.Height;
		mapWidth = newMap.Width;
	}
}
