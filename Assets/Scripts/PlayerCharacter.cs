using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character {
    public int stamina;
    public int maxStamina;
    private Slider staminaSlider;
    public GameObject item;
    public Inventory inventory;
    public string itemname; //Variable for getting specific item
    private bool touching = false;
    public LayerMask ItemLayer; //Check if the object is an item

    protected override void Awake ()
    {
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        healthSlider = GameObject.Find("HealthSlider");
        staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        if (movementEnabled)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(controller.map.TileAbove(currentTile));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Move(controller.map.TileBelow(currentTile));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Move(controller.map.TileLeft(currentTile));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(controller.map.TileRight(currentTile));
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Attack(controller.map.TileAboveStrict(currentTile), 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Attack(controller.map.TileBelowStrict(currentTile), 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Attack(controller.map.TileLeftStrict(currentTile), 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Attack(controller.map.TileRightStrict(currentTile), 1, 1);
        }

        //Action button
        if (Input.GetKeyDown(KeyCode.E))
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
    }

    // Move the player to another tile and actiate it
    public override void Move (int moveTo)
    {
        switch (controller.map.tiles[moveTo].GetComponent<Tile>().type)
        {
            case TileType.Ground:
                currentTile = moveTo;
                GameObject tile = controller.map.tiles[currentTile];
                transform.position = tile.transform.position;
                if (tile.GetComponent<Tile>().attackID == 1)
                {
                    ChangeHealth(health - tile.GetComponent<Tile>().attackDamage);
                }
                break;
            case TileType.Wall:
                break;
            case TileType.Entrance:
                controller.map.tiles[moveTo].GetComponent<Entrance>().TeleportPlayer();
                break;
            case TileType.Shop:
                movementEnabled = false;
                ShopUI s = GameObject.Find("Canvas").transform.Find("ShopWindow").GetComponent<ShopUI>();
                s.SetShop(controller.map.tiles[moveTo].GetComponent<Shop>());
                s.isOpen = true;
                break;
        }
        ChangeStamina(Random.Range(0, 100));
        GameObject.FindWithTag("MainCamera").GetComponent<CameraTracking>().UpdatePos(new Vector3(transform.position.x, transform.position.y, -10));
    }

    // Changes stamina to the given value
    public void ChangeStamina (int newStamina)
    {
        stamina = newStamina;
        staminaSlider.value = (float)stamina / maxStamina * 100;
    }

    //Detects pickupable object
    public void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            item = gameObject;
            touching = true;
        }
    }

    //Detects when leaving pickupable range of object
    public void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            touching = false;
        }
    }
}

