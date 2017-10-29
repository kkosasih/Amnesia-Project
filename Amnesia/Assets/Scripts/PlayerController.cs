using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public Tile currentTile;

	// Use this for initialization
	void Start ()
    {
        currentTile = GameController.map.tiles[GameController.map.playerLoc];
        player.transform.position = currentTile.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            shiftSave(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            shiftSave(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            shiftSave(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            shiftSave(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            shiftSave(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            shiftSave(5);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            shiftSave(6);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            shiftSave(7);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            shiftSave(8);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            shiftSave(9);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            movePlayer(GameController.map.tileAbove(GameController.map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movePlayer(GameController.map.tileBelow(GameController.map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movePlayer(GameController.map.tileLeft(GameController.map.playerLoc));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movePlayer(GameController.map.tileRight(GameController.map.playerLoc));
        }
    }

    private void shiftSave (int slot)
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

    private void movePlayer (int moveTo)
    {
        if (!GameController.map.tiles[moveTo].obstruct)
        {
            GameController.map.playerLoc = moveTo;
            currentTile = GameController.map.tiles[moveTo];
            player.transform.position = currentTile.position;
        }
    }
}
