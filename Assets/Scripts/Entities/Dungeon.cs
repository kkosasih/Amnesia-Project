using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    #region Attributes
    public static Dungeon instance;     // The instance to reference
    public List<DungeonRoom> rooms;     // The rooms of the dungeons
    public int roomIndex = 0;           // The current room to track
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
        if (instance == null)
        {
            instance = this;
        }
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
    
	#endregion
	
	#region Coroutines
	
	#endregion
}
