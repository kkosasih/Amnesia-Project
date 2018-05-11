using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    #region Attributes
    public static CameraTracking instance;
    public Transform track; // The transform to track
    public Vector3 zeroPoint;
	private float camHeight;
	private float camWidth;
    #endregion

    #region Event Functions
    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start ()
    {
        zeroPoint = Vector3.zero;
        Camera cam = GetComponent<Camera>();
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameController.instance.map != null)
        {
            Vector3 playerPos = track.position - zeroPoint;
            transform.position = zeroPoint + new Vector3(
                GameController.instance.map.Width > 2 * camWidth ? Mathf.Clamp(playerPos.x, camWidth - 0.5f, GameController.instance.map.Width - camWidth - 0.5f) : GameController.instance.map.Width / 2f - 0.5f,
                GameController.instance.map.Height > 2 * camHeight ? Mathf.Clamp(playerPos.y, -(GameController.instance.map.Height - camHeight - 0.5f), -(camHeight - 0.5f)) : -(GameController.instance.map.Height / 2f - 0.5f),
                -10);
        }
		//transform.position = new Vector3(track.position.x, track.position.y, - 10);
    }
    #endregion

	public void UpdateMap()
	{
        zeroPoint = GameController.instance.map.transform.position;
	}
}
