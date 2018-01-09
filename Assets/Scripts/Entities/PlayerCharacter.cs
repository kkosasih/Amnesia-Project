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
    public GameObject Questtracking;
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
        if (movementPreventions == 0)
        {
            if (lastMove >= delay && !moving)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Move(GameController.map.TileAbove(currentTile), Direction.Up);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Move(GameController.map.TileBelow(currentTile), Direction.Down);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Move(GameController.map.TileLeft(currentTile), Direction.Left);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Move(GameController.map.TileRight(currentTile), Direction.Right);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(Attack(Direction.Up));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(Attack(Direction.Down));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(Attack(Direction.Left));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(Attack(Direction.Right));
            }
        }

        //Action button
        if (Input.GetKeyDown(KeyCode.E) && movementPreventions == 0)
        {
            switch (GameController.map.tiles[currentTile].GetComponent<Tile>().type)
            {
                case TileType.Sign:
                    GameController.map.tiles[currentTile].GetComponent<Sign>().ReadSign();
                    Questtracking.GetComponent<QuestTracking>().speakobj();
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

    // Move the character to another tile with an animation
    public override void Move (int moveTo, Direction dir)
    {
        base.Move(moveTo, dir);
        ChangeStamina(Random.Range(0, 100));
    }

    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        _animator.SetInteger("direction", (int)dir);
        yield return new WaitForSeconds(0.5f);
        AttackController.instance.StraightAttack(new Attack(teamID, 1, 0.2f), dir, currentTile, 5, 2, 5);
        --movementPreventions;
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

