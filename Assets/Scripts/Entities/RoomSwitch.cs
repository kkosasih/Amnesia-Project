using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : StaticObject {
    #region Attributes
    public int roomTo;          // The room to move to
    public int tileFrom;        // The tile in the room to start in
    public Direction moveTo;    // The direction to move after
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
        GetComponent<Interactible>().interact = SwitchRoom;
    }

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}
	#endregion
	
	#region Methods
	// Move the camera to the next room
    public void SwitchRoom ()
    {
        Dungeon.instance.roomIndex = roomTo;
        PlayerCharacter.instance.startTile = tileFrom;
        GameController.instance.FindMap();
        PlayerCharacter.instance.PlaceOnMap(PlayerCharacter.instance.startTile);
        CameraTracking.instance.UpdateMap();
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
