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
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
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
                Move(GameController.map.TileAbove(currentTile));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Move(GameController.map.TileBelow(currentTile));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Move(GameController.map.TileLeft(currentTile));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(GameController.map.TileRight(currentTile));
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AttackController.instance.StraightAttack(new Attack(teamID, 1, 1), Direction.Up, currentTile, 5, 2, 5);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AttackController.instance.StraightAttack(new Attack(teamID, 1, 1), Direction.Down, currentTile, 5, 2, 5);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AttackController.instance.StraightAttack(new Attack(teamID, 1, 1), Direction.Left, currentTile, 5, 2, 5);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AttackController.instance.StraightAttack(new Attack(teamID, 1, 1), Direction.Right, currentTile, 5, 2, 5);
        }

        //Action button
        if (Input.GetKeyDown(KeyCode.E) && movementPreventions == 0)
        {
            switch (GameController.map.tiles[currentTile].GetComponent<Tile>().type)
            {
                case TileType.Sign:
                    GameController.map.tiles[currentTile].GetComponent<Sign>().ReadSign();
                    break;
                case TileType.Pickup:
                    pickupui.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        if (onMap && movementPreventions == 0)
        {
            switch (GameController.map.tiles[currentTile].GetComponent<Tile>().type)
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
        else
        {
            interactionbutton.gameObject.SetActive(false);
        }
    }

    // Move the player to another tile and actiate it
    public override void Move (int moveTo)
    {
        base.Move(moveTo);
        ChangeStamina(Random.Range(0, 100));
    }

    // Performs all special actions that a tile would perform
    protected override void HandleTile ()
    {
        switch (GameController.map.tiles[currentTile].GetComponent<Tile>().type)
        {
            case TileType.Entrance:
                StartCoroutine(GameController.map.tiles[currentTile].GetComponent<Entrance>().TeleportPlayer());
                break;
            case TileType.Shop:
                ShopUI s = GameObject.Find("ShopWindow").GetComponent<ShopUI>();
                s.SetShop(GameController.map.tiles[currentTile].GetComponent<Shop>());
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

