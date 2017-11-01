using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public int currentTile;
    public Map map;

    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        FindMap();
        MovePlayer(currentTile);
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().FindMap();

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
            MovePlayer(map.TileAbove(currentTile));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(map.TileBelow(currentTile));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(map.TileLeft(currentTile));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(map.TileRight(currentTile));
        }
    }

    // Attach a map to the player to use
    public void FindMap ()
    {
        map = GameObject.FindWithTag("Map").GetComponent<Map>();
        Debug.Log(map);
    }

    // Move the player to a different tile
    public void MovePlayer (int moveTo)
    {
        if (map.tiles[moveTo].GetComponent<Tile>().type != 1)
        {
            currentTile = moveTo;
            player.transform.position = map.tiles[currentTile].transform.position;
            if (map.tiles[currentTile].GetComponent<Tile>().type == 2)
            {
                map.tiles[currentTile].GetComponent<Entrance>().TeleportPlayer();
            }
        }

        GameObject.FindWithTag("Player").GetComponent<PlayerStats>().ChangeStamina(Random.Range(0, 100));
    }

    // Save and load based on shift key
    private void ShiftSave (int slot)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SaveLoad.Save(slot);
        }
        else
        {
            SaveLoad.Load(slot);
        }
    }
}
