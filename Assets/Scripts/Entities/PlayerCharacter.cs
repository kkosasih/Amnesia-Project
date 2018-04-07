using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character {
    #region Attributes
    public static PlayerCharacter instance; // The instance script to reference
    public int stamina;                     // The current stamina of the player
    [SerializeField]
    private int maxStamina;                 // The max stamina of the player
    [SerializeField]
	private int staminaDepletionAttack;     // How much stamina is depleted when player attacks
    [SerializeField]
    private GameObject interactionbutton;
    [SerializeField]
    private GameObject pickupui;
    [SerializeField]
    private GameObject Questtracking;
    private Slider staminaSlider;           // The slider object to reference for stamina
    private Interactible interaction;       // The interaction available
    private Inventory inven;            // The inventory of the player
    private AudioSource footstepsAudio;     // The audio played when the player moves
    #endregion

    #region Properties
    // Returns maxStamina
    public int MaxStamina
    {
        get
        {
            return maxStamina;
        }
    }

    // Returns inven
    public Inventory Inven
    {
        get
        {
            return inven;
        }
    }
    #endregion

    #region Event Functions
    protected override void Awake ()
    {
        // Set up other scripts
        inven = GameObject.Find("Inventory").GetComponent<Inventory>();
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
        if (OnMap && DialogueController.instance.MovementPreventions + movementPreventions == 0)
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
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    StartCoroutine(Attack(Direction.Up));
                }
                else
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(Attack(Direction.Down));
                }
                else
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    StartCoroutine(Attack(Direction.Left));
                }
                else
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(Attack(Direction.Right));
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

            //Action button uses
            if (interaction != null)
            {
                if (!interaction.automatic)
                {
                    interactionbutton.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interaction.interact();
                }
                else if (interaction.automatic)
                {
                    interaction.interact();
                    interaction = null;
                }
            }
            else
            {
                interactionbutton.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Methods
    // Changes stamina to the given value
    public void ChangeStamina (int newStamina)
    {
        stamina = newStamina;
        staminaSlider.value = (float)stamina / maxStamina * 100;
    }

    // Updates the interaciton status
    public void UpdateInteraction ()
    {
        foreach (StaticObject s in GameController.map.TakenTiles.Keys)
        {
            Interactible result = s.gameObject.GetComponent<Interactible>();
            if (result != null && s.TotalDistance(CurrentTile) <= result.range)
            {
                interaction = result;
            }
        }
        interaction = null;
    }
    #endregion

    #region Coroutines
    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        _animator.SetInteger("direction", (int)dir);
        yield return new WaitForSeconds(0.5f);
        AttackController.instance.StraightAttack(new Attack(teamID, 1, 0.2f), dir, CurrentTile, 5, 2, 5);
        --movementPreventions;
        ChangeStamina(stamina - staminaDepletionAttack);
    }
    #endregion
}

