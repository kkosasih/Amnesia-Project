using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : StaticObject {
    public int teamID;                      // The team that the character belongs to; no friendly fire
    public int movementPreventions = 0;     // 0 if not in a cutscene, a number otherwise
    public bool moving = false;             // Whether the character is moving
    public float delay;                     // How long it takes to move between tiles
    public float lastMove = 0.0f;           // Time since the last movement
    public int lastTile = -1;               // The last tile the character was on
    public int health;                      // The health of the character
    public int maxHealth;                   // The maximum health of the character
    public GameObject healthSlider;         // The slider to reference for health
    protected bool attacked = false;        // Whether the character has been hit recently
    protected Coroutine movementRoutine;    // The animation coroutine to play/stop
    protected Animator _animator;           // The Animator component attached

    protected virtual void Awake ()
    {
        healthSlider = Instantiate((GameObject)Resources.Load("GUI/CharHealthSlider"), GameObject.Find("DynamicCanvas").transform);
        UITracking uit = healthSlider.GetComponent<UITracking>();
        uit.obj = gameObject;
        uit.offset = new Vector3(0, 40, 0);
        _animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	protected virtual void Update ()
    {
        if (onMap)
        {
            if (lastMove < delay)
            {
                lastMove += Time.deltaTime;
            }
            if (TileHurts() && !attacked && GameController.map.tiles[currentTile].GetComponent<Tile>().Effect() == StatusEffect.empty)
            {
                ChangeHealth(health - GameController.map.tiles[GameController.map.takenTiles[this]].GetComponent<Tile>().Damage(teamID));
                attacked = true;
            }
            else if (TileHurts() && !attacked && GameController.map.tiles[currentTile].GetComponent<Tile>().Effect() != StatusEffect.empty)
            {
                ChangeHealth(health - GameController.map.tiles[currentTile].GetComponent<Tile>().Damage(teamID));
                StartCoroutine(ApplyStatusEffect(GameController.map.tiles[currentTile].GetComponent<Tile>().Effect()));
                attacked = true;
            }
            else if (!TileHurts())
            {
                attacked = false;
            }
        }
        if (health <= 0)
        {
            Die();
        }
	}

    // Move the character to an adjacent tile with an animation
    public virtual void Move (Direction dir)
    {
        _animator.SetInteger("direction", (int)dir);
        int moveTo = -1;
        switch (dir)
        {
            case Direction.Up:
                moveTo = GameController.map.TileAboveStrict(GameController.map.takenTiles[this]);
                break;
            case Direction.Down:
                moveTo = GameController.map.TileBelowStrict(GameController.map.takenTiles[this]);
                break;
            case Direction.Left:
                moveTo = GameController.map.TileLeftStrict(GameController.map.takenTiles[this]);
                break;
            case Direction.Right:
                moveTo = GameController.map.TileRightStrict(GameController.map.takenTiles[this]);
                break;
        }
        if (GameController.map.tiles[moveTo].GetComponent<Tile>().type != TileType.Wall && !GameController.map.TileIsTaken(moveTo))
        {
            lastMove = 0.0f;
            lastTile = GameController.map.takenTiles[this];
            StartCoroutine(ChangeTile(moveTo));
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
            }
            movementRoutine = StartCoroutine(Helper.PlayInTime(GetComponent<Animator>(), "moving", true, false, Mathf.Min(0.5f, delay)));
        }
    }

    // Move the character to another tile with an animation
    public virtual void Move (int moveTo, Direction dir)
    {
        _animator.SetInteger("direction", (int)dir);
        if (GameController.map.tiles[moveTo].GetComponent<Tile>().type != TileType.Wall && !GameController.map.TileIsTaken(moveTo))
        {
            lastMove = 0.0f;
            lastTile = GameController.map.takenTiles[this];
            StartCoroutine(ChangeTile(moveTo));
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
            }
            movementRoutine = StartCoroutine(Helper.PlayInTime(GetComponent<Animator>(), "moving", true, false, Mathf.Min(0.5f, delay)));
        }
    }

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
        List<MapNode> path = GameController.map.FindPath(node, GameController.map.NodeTileIn(destination));
        // Move to the correct node
        for (int i = 1; i < path.Count; ++i)
        { 
            int step = node.GateToNode(path[i], GameController.map.takenTiles[this]);
            while (GameController.map.takenTiles[this] != step)
            {
                Move(DirectionToward(step));
                yield return new WaitForSeconds(delay + 0.05f);
            }
            Move(node.adjacentNodes[path[i]]);
            yield return new WaitForSeconds(delay + 0.05f);
        }
        // Move to the correct tile within the node
        while (GameController.map.takenTiles[this] != destination)
        {
            Move(DirectionToward(destination));
            yield return new WaitForSeconds(delay + 0.05f);
        }
    }

    // Move the character to a destination by one step
    public IEnumerator AutoMoveOneStep (int destination)
    {
        List<MapNode> path = GameController.map.FindPath(node, GameController.map.NodeTileIn(destination));
        // Move toward correct node
        if (path.Count > 1)
        {
            int gateNode = node.GateToNode(path[1], GameController.map.takenTiles[this]);
            if (GameController.map.takenTiles[this] != gateNode)
            {
                Move(DirectionToward(node.GateToNode(path[1], GameController.map.takenTiles[this])));
            }
            else
            {
                Move(node.adjacentNodes[path[1]]);
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

    // Kill this character
    public override void Die ()
    {
        Destroy(healthSlider);
        base.Die();
    }

    // Change the health of the character to the new value
    public void ChangeHealth (int newHealth)
    {
        if (newHealth < health)
        {
            StartCoroutine(LoseHealth(newHealth));
        }
        else if (newHealth > health)
        {
            StartCoroutine(GainHealth(newHealth));
        }
        health = newHealth;
    }

    // Has the health bar react to gaining health
    private IEnumerator GainHealth (int newHealth)
    {
        healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
        healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
        yield return new WaitForSeconds(1);
        healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
    }

    // Has the health bar react to losing health
    private IEnumerator LoseHealth (int newHealth)
    {
        healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
        healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0.5f, 0, 0);
        yield return new WaitForSeconds(1);
        healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
    }

    // Performs all of the movement to another tile
    protected virtual IEnumerator ChangeTile (int moveTo)
    {
        GameController.map.takenTiles[this] = moveTo;
        node = GameController.map.NodeTileIn(GameController.map.takenTiles[this]);
        moving = true;
        Vector3 oldPos = transform.position;
        Vector3 newPos = GameController.map.tiles[moveTo].transform.position;
        for (float timePassed = 0; timePassed < Mathf.Min(0.5f, delay); timePassed += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timePassed / Mathf.Min(0.5f, delay));
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        HandleTile();
        moving = false;
        PlayerCharacter.instance.interaction = GameController.map.FindInteractible();
    }

    // Applies the status effect
    private IEnumerator ApplyStatusEffect(StatusEffect effect)
    {
        float currentTime = 0.0f;
        while (currentTime <= effect.duration)
        {
            ChangeHealth(health - effect.damage);
            yield return new WaitForSeconds(effect.repeatTime);
            currentTime += effect.repeatTime;
        }
    }

    // Performs all special actions that a tile would perform if moved to
    protected virtual void HandleTile ()
    {

    }

    // Checks if the tile is harmful to the character
    protected bool TileHurts ()
    {
        return GameController.map.tiles[GameController.map.takenTiles[this]].GetComponent<Tile>().Damage(teamID) > 0;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Invalid
}