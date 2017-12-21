using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character {
    public int stamina;
    public int maxStamina;
    private Slider staminaSlider;
    public GameObject item;
    public GameObject interactionbutton;
    public GameObject pickupui;
    public Inventory inventory;
    public string itemname; //Variable for getting specific item
    private bool touching = false;
    public LayerMask ItemLayer; //Check if the object is an item

    protected override void Awake ()
    {
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        interactionbutton = GameObject.Find("InteractionIndicator");
        healthSlider = GameObject.Find("HealthSlider");
        staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        if (movementPreventions == 0 && lastMove >= delay)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Move(controller.map.TileAbove(currentTile));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Move(controller.map.TileBelow(currentTile));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Move(controller.map.TileLeft(currentTile));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(controller.map.TileRight(currentTile));
            }
        }
        
        if (Input.GetKeyDown(KeyCode.M))//Keypad8))
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
        if (controller.map.tiles[currentTile].GetComponent<Tile>().attackID == 2 && !attacked)
        {
            ChangeHealth(health - controller.map.tiles[currentTile].GetComponent<Tile>().attackDamage);
            attacked = true;
        }
        else if (controller.map.tiles[currentTile].GetComponent<Tile>().attackID == 0)
        {
            attacked = false;
        }

        //Action button
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch(controller.map.tiles[currentTile].GetComponent<Tile>().type)
            {
                case TileType.Sign:

                    break;
                case TileType.Pickup:
                    pickupui.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        switch(controller.map.tiles[currentTile].GetComponent<Tile>().type)
        {
            case TileType.Sign:
                interactionbutton.gameObject.SetActive(true);
                break;
            case TileType.Pickup:
                interactionbutton.gameObject.SetActive(true);
                break;
            default:
                interactionbutton.gameObject.SetActive(false);
                break;
        }
    }

    // Move the player to another tile and actiate it
    public override void Move (int moveTo)
    {
        base.Move(moveTo);
        ChangeStamina(Random.Range(0, 100));
    }

    // Performs all special actions that a tile would perform
    protected override void HandleTile()
    {
        switch (controller.map.tiles[currentTile].GetComponent<Tile>().type)
        {
            case TileType.Entrance:
                controller.map.tiles[currentTile].GetComponent<Entrance>().TeleportPlayer();
                break;
            case TileType.Shop:
                ShopUI s = GameObject.Find("ShopWindow").GetComponent<ShopUI>();
                s.SetShop(controller.map.tiles[currentTile].GetComponent<Shop>());
                s.EnterShop();
                break;
        }
    }

    // Changes stamina to the given value
    public void ChangeStamina (int newStamina)
    {
        stamina = newStamina;
        staminaSlider.value = (float)stamina / maxStamina * 100;
    }
}

