﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : Map {
    #region Attributes
    [SerializeField]
    protected string filePath;  // The path to the special tiles file
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	protected override void Awake ()
	{
        // Find and read the mapping image
        Texture2D mapLayout = Resources.Load<Texture2D>("Maps/Textures/" + path);
        List<Color> data = Helper.ReverseInChunks(new List<Color>(mapLayout.GetPixels()), mapLayout.width);
        string[] entries = Resources.Load<TextAsset>("Maps/Tiles/" + filePath).text.Split(new string[] { "..." }, System.StringSplitOptions.None);
        width = mapLayout.width;
        height = mapLayout.height;
        tiles = new List<GameObject>(width * height);
        takenTiles = new Dictionary<StaticObject, int>();
        // Set up special tiles
        List<int> roomSwitches = new List<int>();
        // Match colors in the picture to tile type
        for (int i = 0; i < data.Count; ++i)
        {
            if (data[i] == new Color(0, 1, 0))
            {
                tiles.Add(Instantiate(Resources.Load<GameObject>("Tiles/Ground Tile"), transform));
            }
            else if (data[i] == new Color(1, 0, 0))
            {
                tiles.Add(Instantiate(Resources.Load<GameObject>("Tiles/Wall Tile"), transform));
            }
            else if (data[i] == new Color(1, 1, 1))
            {
                tiles.Add(Instantiate(Resources.Load<GameObject>("Tiles/Switch Tile"), transform));
                roomSwitches.Add(i);
            }
            else
            {
                Debug.Log("Tile at (" + (i % width).ToString() + ", " + (i / width).ToString() + ") is not valid. Color is " + data[i].ToString());
            }
            tiles[i].GetComponent<Tile>().UpdatePosition(new Vector2(i % width, -i / width));
        }
        // Add in the special tiles to the given locations
        int roomsI = 0;
        if (entries[0].Trim().Length != 0)
        {
            Tile t;
            for (int i = 0; i < entries.Length; ++i)
            {
                switch (entries[i].Trim()[0])
                {
                    case 'R':
                        RoomSwitch r = tiles[roomSwitches[roomsI]].GetComponent<RoomSwitch>();
                        string[] args = entries[i].Split(':')[1].Split(',');
                        r.roomTo = int.Parse(args[0]);
                        r.tileFrom = int.Parse(args[1]);
                        r.moveTo = (Direction)int.Parse(args[2]);
                        t = tiles[roomSwitches[roomsI++]].GetComponent<Tile>();
                        r.PlaceOnMap(this, -(int)t.position.y * width + (int)t.position.x);
                        break;
                    default:
                        Debug.Log("Invalid entry type");
                        break;
                }
            }
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
