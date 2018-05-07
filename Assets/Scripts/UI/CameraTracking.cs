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
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameController.instance.map != null)
        {
             transform.position = new Vector3(
                 GameController.instance.map.Width > 2 * camWidth ? Mathf.Clamp(track.position.x, camWidth - 0.5f, GameController.instance.map.Width - camWidth - 0.5f) : GameController.instance.map.Width / 2f - 0.5f,
                 GameController.instance.map.Height > 2 * camHeight ? Mathf.Clamp(track.position.y, -(GameController.instance.map.Height - camHeight - 0.5f), -(camHeight - 0.5f)) : -(GameController.instance.map.Height / 2f - 0.5f),
                 -10);
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
