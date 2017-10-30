using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public GameObject currentTile;
    public Map map;

	// Use this for initialization
	void Start ()
    { 
        MovePlayer(map.playerLoc);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ShiftSave(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ShiftSave(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ShiftSave(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ShiftSave(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ShiftSave(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ShiftSave(5);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ShiftSave(6);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            ShiftSave(7);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ShiftSave(8);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            ShiftSave(9);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(map.TileAbove(map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(map.TileBelow(map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(map.TileLeft(map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(map.TileRight(map.playerLoc));
        }
    }

    public void MovePlayer(int moveTo)
    {
        if (map.tiles[moveTo].GetComponent<Tile>().type != 1)
        {
            map.playerLoc = moveTo;
            currentTile = map.tiles[moveTo];
            player.transform.position = currentTile.transform.position;
            if (map.tiles[moveTo].GetComponent<Tile>().type == 2)
            {
                Entrance.TeleportPlayer();
            }
        }
    }

    private void ShiftSave (int slot)
    {
        if (Input.GetKey(KeyCode.S))
        {
            SaveLoad.Save(slot);
        }
        else
        {
            SaveLoad.Load(slot);
        }
    }
}
