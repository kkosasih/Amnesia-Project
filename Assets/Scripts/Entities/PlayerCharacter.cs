using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character {
    public static PlayerCharacter instance; // The instance script to reference
    public int stamina;                     // The current stamina of the player
    public int maxStamina;                  // The max stamina of the player
	public int staminaDepletionAttack;      // How much stamina is depleted when player attacks
    private Slider staminaSlider;           // The slider object to reference for stamina
    public GameObject interactionbutton;
    public GameObject pickupui;
    public GameObject Questtracking;
    public Inventory inventory;             // The inventory of the player
    public string itemname;                 // Variable for getting specific item
    //private bool touching = false;
    public LayerMask ItemLayer;             // Check if the object is an item
    private AudioSource footstepsAudio;     // The audio played when the player moves

    protected override void Awake ()
    {
        // Set up other scripts
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        healthSlider = GameObject.Find("HealthSlider");
        staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
        footstepsAudio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        // If not in a cutscene
        if (movementPreventions == 0)
        {
            // If not moving at the moment
            if (lastMove >= delay && !moving)
            {
                // Move controls
                if (Input.GetKey(KeyCode.W))
                {
                    Move(Direction.Up);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Move(Direction.Down);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Move(Direction.Left);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Move(Direction.Right);
                }
            }
            // Attack controls
			if (stamina >= staminaDepletionAttack)
			{
				if (Input.GetKeyDown (KeyCode.UpArrow))
				{
					StartCoroutine (Attack (Direction.Up));
				}
				else
				if (Input.GetKeyDown (KeyCode.DownArrow))
				{
					StartCoroutine (Attack (Direction.Down));
				}
				else
				if (Input.GetKeyDown (KeyCode.LeftArrow))
				{
					StartCoroutine (Attack (Direction.Left));
				}
				else
				if (Input.GetKeyDown (KeyCode.RightArrow))
				{
					StartCoroutine (Attack (Direction.Right));
				}
			}
            if (moving && !footstepsAudio.isPlaying)
            {
                footstepsAudio.time = 0.5f;
                footstepsAudio.Play();
            }
            else if (!moving && footstepsAudio.isPlaying)
            {
                footstepsAudio.Stop();
            }
        }

        //Action button uses
        if (Input.GetKeyDown(KeyCode.E) && movementPreventions == 0)
        {
            switch (GameController.map.tiles[currentTile].GetComponent<Tile>().type)
            {
                case TileType.Sign:
                    GameController.map.tiles[currentTile].GetComponent<Sign>().ReadSign();
                    break;
                case TileType.Pickup:
                    pickupui.GetComponent<PickupItemScreen>().ChangeInventory(GameController.map.tiles[currentTile].GetComponent<PickupInventory>());
                    pickupui.GetComponent<PickupItemScreen>().Open();
                    break;
				case TileType.Bed:
				StartCoroutine(GameController.map.tiles [currentTile].GetComponent<Bed> ().sleep ());
					break;
                default:
                    break;
            }
        }
        // Interaction button situations
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
				case TileType.Bed:
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

    // Move the character to an adjacent tile with an animation
    public override void Move(Direction dir)
    {
        base.Move(dir);
    }

    // Move the character to another tile with an animation
    public override void Move (int moveTo, Direction dir)
    {
        base.Move(moveTo, dir);
        //ChangeStamina(Random.Range(0, 100));
    }

    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        _animator.SetInteger("direction", (int)dir);
        yield return new WaitForSeconds(0.5f);
        AttackController.instance.StraightAttack(new Attack(teamID, 1, 0.2f), dir, currentTile, 5, 2, 5);
        --movementPreventions;
		ChangeStamina (stamina - staminaDepletionAttack);
    }

    // Performs all special actions that a tile would perform when walking on it
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

