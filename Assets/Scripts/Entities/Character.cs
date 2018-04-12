using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : StaticObject {
    #region Attributes
    public int health;                      // The health of the character
    [SerializeField]
    protected int teamID;                   // The team that the character belongs to; no friendly fire
    [SerializeField]
    protected float delay;                  // How long it takes to move between tiles
    [SerializeField]
    protected int maxHealth;                // The maximum health of the character
    [SerializeField]
    protected Transform bodyParts;          // The body parts of the characters
    protected List<Animator> animators;     // The animators for the body parts
    protected GameObject healthSlider;      // The slider to reference for health
    protected int movementPreventions = 0;  // 0 if not in a cutscene, a number otherwise
    protected bool moving = false;          // Whether the character is moving
    protected float lastMove = 0.0f;        // Time since the last movement
    //public int lastTile = -1;               // The last tile the character was on
    protected bool attacked = false;        // Whether the character has been hit recently
    protected Coroutine movementRoutine;    // The animation coroutine to play/stop
    #endregion

    #region Properties
    // Returns health and sets health depending on death conditions
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            StartCoroutine(UpdateHealthBar());
            if (health <= 0)
            {
                Die();
            }
        }
    }
    
    // Returns maxHealth
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    // Returns movementPreventions and clamps minimum to 0
    public int MovementPreventions
    {
        get
        {
            return movementPreventions;
        }
        set
        {
            movementPreventions = Mathf.Max(0, value);
        }
    }

    // Returns whether the tile that the character is standing on hurts
    private bool TileHurts
    {
        get
        {
            return GameController.instance.map.Tiles[CurrentTile].GetComponent<Tile>().Damage(teamID) > 0 || GameController.instance.map.Tiles[CurrentTile].GetComponent<Tile>().Effect().damage > 0;
        }
    }

    // Returns the total preventions taking into account the static ones
    public int Preventions
    {
        get
        {
            return movementPreventions + DialogueController.instance.MovementPreventions;
        }
    }
    #endregion

    #region Event Functions
    protected virtual void Awake ()
    {
        healthSlider = Instantiate((GameObject)Resources.Load("GUI/CharHealthSlider"), GameObject.Find("DynamicCanvas").transform);
        UITracking uit = healthSlider.GetComponent<UITracking>();
        uit.obj = gameObject;
        uit.offset = new Vector3(0, 40, 0);
        animators = new List<Animator>(bodyParts.GetComponentsInChildren<Animator>());
        Animator a = GetComponent<Animator>();
        if (a != null)
        {
            animators.Add(a);
        }
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        if (OnMap)
        {
            if (lastMove < delay)
            {
                lastMove += Time.deltaTime;
            }
            if (TileHurts && !attacked)
            {
                StartCoroutine(ApplyStatusEffect(GameController.instance.map.Tiles[CurrentTile].GetComponent<Tile>().Effect()));
                ChangeHealth(-GameController.instance.map.Tiles[CurrentTile].GetComponent<Tile>().Damage(teamID));
                attacked = true;
            }
            else if (!TileHurts)
            {
                attacked = false;
            }
        }
	}
    #endregion

    #region Methods
    // Move the character to an adjacent tile with an animation
    public virtual void Move (Direction dir)
    {
        SetAllIntegers("direction", (int)dir);
        int moveTo = -1;
        switch (dir)
        {
            case Direction.Up:
                moveTo = GameController.instance.map.TileAboveStrict(CurrentTile);
                break;
            case Direction.Down:
                moveTo = GameController.instance.map.TileBelowStrict(CurrentTile);
                break;
            case Direction.Left:
                moveTo = GameController.instance.map.TileLeftStrict(CurrentTile);
                break;
            case Direction.Right:
                moveTo = GameController.instance.map.TileRightStrict(CurrentTile);
                break;
        }
        if (GameController.instance.map.Tiles[moveTo].GetComponent<Tile>().type != TileType.Wall && !GameController.instance.map.TileIsTaken(moveTo))
        {
            lastMove = 0.0f;
            StartCoroutine(ChangeTile(moveTo));
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
            }
            movementRoutine = StartCoroutine(Helper.PlayInTime(animators, "moving", true, false, Mathf.Min(0.5f, delay)));
        }
    }

    // Move the character to another tile with an animation
    public virtual void Move (int moveTo, Direction dir)
    {
        SetAllIntegers("direction", (int)dir);
        if (GameController.instance.map.Tiles[moveTo].GetComponent<Tile>().type != TileType.Wall && !GameController.instance.map.TileIsTaken(moveTo))
        {
            lastMove = 0.0f;
            StartCoroutine(ChangeTile(moveTo));
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
            }
            movementRoutine = StartCoroutine(Helper.PlayInTime(animators, "moving", true, false, Mathf.Min(0.5f, delay)));
        }
    }

    // Kill this character
    public override void Die ()
    {
        Destroy(healthSlider);
        base.Die();
    }

    // Change the health of the character to the new value
    public void SetHealth (int newHealth)
    {
        Health = newHealth;
    }

    // Change the health of the character by a given value
    public void ChangeHealth (int change)
    {
        SetHealth(health + change);
    }

    // Performs all special actions that a tile would perform if moved to
    protected virtual void HandleTile ()
    {

    }

    // Sets a boolean for all animators
    protected void SetAllBools (string name, bool value)
    {
        foreach (Animator a in animators)
        {
            a.SetBool(name, value);
        }
    }

    // Sets an integer for all animators
    protected void SetAllIntegers (string name, int value)
    {
        foreach (Animator a in animators)
        {
            a.SetInteger(name, value);
        }
    }

    // Sets a float for all animators
    protected void SetAllFloats (string name, float value)
    {
        foreach (Animator a in animators)
        {
            a.SetFloat(name, value);
        }
    }
    #endregion

    #region Coroutines
    // Move the character by multiple tiles automatically
    public IEnumerator AutoMove (List<Direction> path)
    {
        for (int i = 0; i < path.Count; ++i)
        {   
            // Move for each step
            Move(path[i]);
            if (i < path.Count - 1)
            {
                yield return new WaitForSeconds(delay);
            } 
        }
    }

    // Move the character to a specific tile automatically
    public IEnumerator AutoMove (int destination)
    {
        List<MapNode> path = GameController.instance.map.FindPath(Node, GameController.instance.map.NodeTileIn(destination));
        // Move to the correct node
        for (int i = 1; i < path.Count; ++i)
        { 
            int step = Node.GateToNode(path[i], CurrentTile);
            while (CurrentTile != step)
            {
                Move(DirectionToward(step));
                yield return new WaitForSeconds(delay + 0.05f);
            }
            Move(Node.adjacentNodes[path[i]]);
            yield return new WaitForSeconds(delay + 0.05f);
        }
        // Move to the correct tile within the node
        while (CurrentTile != destination)
        {
            Move(DirectionToward(destination));
            yield return new WaitForSeconds(delay + 0.05f);
        }
    }

    // Move the character to a destination by one step
    public IEnumerator AutoMoveOneStep (int destination)
    {
        List<MapNode> path = GameController.instance.map.FindPath(Node, GameController.instance.map.NodeTileIn(destination));
        // Move toward correct node
        if (path.Count > 1)
        {
            int gateNode = Node.GateToNode(path[1], CurrentTile);
            if (CurrentTile != gateNode)
            {
                Move(DirectionToward(Node.GateToNode(path[1], CurrentTile)));
            }
            else
            {
                Move(Node.adjacentNodes[path[1]]);
            }
        }
        // Or move to the tile directly
        else
        {
            Move(DirectionToward(destination));
        }
        yield return new WaitForSeconds(delay);
    }

    // Attack in a given direction dir
    public virtual IEnumerator Attack (Direction dir)
    {
        yield return null;
    }

    // Has the health bar react to changing health
    private IEnumerator UpdateHealthBar ()
    {
        if (health * 100f / maxHealth > healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value)
        {
            healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
            healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = health * 100f / maxHealth;
            yield return new WaitForSeconds(1);
            healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = health * 100f / maxHealth;
        }
        else
        {
            healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = health * 100f / maxHealth;
            healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0.5f, 0, 0);
            yield return new WaitForSeconds(1);
            healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = health * 100f / maxHealth;
        }
    }

    // Performs all of the movement to another tile
    protected virtual IEnumerator ChangeTile (int moveTo)
    {
        CurrentTile = moveTo;
        PlayerCharacter.instance.UpdateInteraction();
        moving = true;
        Vector3 oldPos = transform.position;
        Vector3 newPos = GameController.instance.map.Tiles[moveTo].transform.position;
        for (float timePassed = 0; timePassed < Mathf.Min(0.5f, delay); timePassed += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timePassed / Mathf.Min(0.5f, delay));
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        HandleTile();
        moving = false;
    }

    // Applies the status effect
    private IEnumerator ApplyStatusEffect (StatusEffect effect)
    {
        float currentTime = 0.0f;
        while (currentTime <= effect.duration)
        {
            ChangeHealth(-effect.damage);
            yield return new WaitForSeconds(effect.repeatTime);
            currentTime += effect.repeatTime;
        }
    }
    #endregion
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Invalid
}