using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public GameObject item;
    public Inventory inventory;
    public int currentTile;
    public string itemname; //Variable for getting specific item
    private bool touching = false;
    public Map map;
    public LayerMask ItemLayer; //Check if the object is an item
    public bool movementEnabled = true;

    // Use this for initialization
    void Start ()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
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

        //Action button
        if(Input.GetKeyDown(KeyCode.E))
        {
            //If object is on Object layer will add object to inventory
            if ((touching == true) && (Physics2D.Raycast(transform.position, transform.forward, 1, ItemLayer)))
            {
                //Not tested
                itemname = item.name;
                inventory.AddItem(itemname);
                DestroyObject(item);
                touching = false;
            }     
        }

        if (movementEnabled)
        {
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack(map.TileAboveStrict(currentTile), 1);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Attack(map.TileBelowStrict(currentTile), 1);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Attack(map.TileLeftStrict(currentTile), 1);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Attack(map.TileRightStrict(currentTile), 1);
        }
    }

    //Detects pickupable object
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            item = gameObject;
            touching = true;
        }
    }

    //Detects when leaving pickupable range of object
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            touching = false;
        }
    }

    // Attach a map to the player to use
    public void FindMap ()
    {
        map = GameObject.FindWithTag("Map").GetComponent<Map>();
        Debug.Log(map);
    }

    // Move the player to a different tile and activate tile properties if necessary
    public void MovePlayer (int moveTo)
    {
        switch (map.tiles[moveTo].GetComponent<Tile>().type)
        {
            case Tile.TileType.Ground:
                currentTile = moveTo;
                player.transform.position = map.tiles[currentTile].transform.position;
                break;
            case Tile.TileType.Wall:
                break;
            case Tile.TileType.Entrance:
                map.tiles[moveTo].GetComponent<Entrance>().TeleportPlayer();
                break;
            case Tile.TileType.Shop:
                movementEnabled = false;
                ShopUI s = GameObject.Find("Canvas").transform.Find("ShopWindow").GetComponent<ShopUI>();
                s.SetShop(map.tiles[moveTo].GetComponent<Shop>());
                s.isOpen = true;
                break;
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerStats>().ChangeStamina(Random.Range(0, 100));
    }

    // Put an attack on a tile for a given time
    public void Attack (int tile, float duration)
    {
        if (tile != -1)
        {
            StartCoroutine(map.tiles[tile].GetComponent<Tile>().GiveAttack(duration));
        }
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
