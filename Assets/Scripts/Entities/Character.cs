using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int teamID;                      // The team that the character belongs to; no friendly fire
    public bool onMap = false;              // Whether the character is placed on the map  
    public MapNode node;                    // The current node that the character is in
    public int currentTile;                 // The tile number that the character is on
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
            if (TileHurts() && !attacked)
            {
                ChangeHealth(health - GameController.map.tiles[currentTile].GetComponent<Tile>().Damage(teamID));
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
                moveTo = GameController.map.TileAboveStrict(currentTile);
                break;
            case Direction.Down:
                moveTo = GameController.map.TileBelowStrict(currentTile);
                break;
            case Direction.Left:
                moveTo = GameController.map.TileLeftStrict(currentTile);
                break;
            case Direction.Right:
                moveTo = GameController.map.TileRightStrict(currentTile);
                break;
        }
        if (GameController.map.tiles[moveTo].GetComponent<Tile>().type != TileType.Wall)
        {
            lastMove = 0.0f;
            lastTile = currentTile;
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
        if (GameController.map.tiles[moveTo].GetComponent<Tile>().type != TileType.Wall)
        {
            lastMove = 0.0f;
            lastTile = currentTile;
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
            int step = node.GateToNode(path[i], currentTile);
            while (currentTile != step)
            {
                Move(DirectionToward(step));
                yield return new WaitForSeconds(delay + 0.05f);
            }
            Move(node.adjacentNodes[path[i]]);
            yield return new WaitForSeconds(delay + 0.05f);
        }
        // Move to the correct tile within the node
        while (currentTile != destination)
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
            int gateNode = node.GateToNode(path[1], currentTile);
            if (currentTile != gateNode)
            {
                Move(DirectionToward(node.GateToNode(path[1], currentTile)));
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
    public virtual void Die ()
    {
        Destroy(healthSlider);
        Destroy(gameObject);
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

    // Find the tile on the map to go to
    public void PlaceOnMap ()
    {
        if (GameController.map != null)
        {
            transform.position = GameController.map.tiles[currentTile].transform.position;
            node = GameController.map.NodeTileIn(currentTile);
            onMap = true;
        }
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
    private IEnumerator ChangeTile (int moveTo)
    {
        moving = true;
        Vector3 oldPos = transform.position;
        Vector3 newPos = GameController.map.tiles[moveTo].transform.position;
        for (float timePassed = 0; timePassed < Mathf.Min(0.5f, delay); timePassed += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timePassed / Mathf.Min(0.5f, delay));
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        currentTile = moveTo;
        node = GameController.map.NodeTileIn(currentTile);
        HandleTile();
        moving = false;
    }

    // Performs all special actions that a tile would perform if moved to
    protected virtual void HandleTile ()
    {

    }

    // Checks if the tile is harmful to the character
    protected bool TileHurts ()
    {
        return GameController.map.tiles[currentTile].GetComponent<Tile>().Damage(teamID) > 0;
    }

    // Get the horizontal distance from a tile; negative is tile to right, positive is tile to left
    protected int HoriDistance (int tile)
    {
        return currentTile % GameController.map.width - tile % GameController.map.width;
    }

    // Get the vertical distance from a tile; negative is tile below, positive is tile above
    protected int VertDistance (int tile)
    {
        return currentTile / GameController.map.width - tile / GameController.map.width;
    }

    // Get the direction to move in towards a certain tile
    protected Direction DirectionToward (int destination)
    {
        int hDis = HoriDistance(destination);
        int vDis = VertDistance(destination);
        if (Mathf.Abs(vDis) > Mathf.Abs(hDis) && vDis > 0)
        {
            return Direction.Up;
        }
        else if (Mathf.Abs(vDis) > Mathf.Abs(hDis) && vDis < 0)
        {
            return Direction.Down;
        }
        else if (Mathf.Abs(hDis) >= Mathf.Abs(vDis) && hDis > 0)
        {
            return Direction.Left;
        }
        else if (Mathf.Abs(hDis) >= Mathf.Abs(vDis) && hDis < 0)
        {
            return Direction.Right;
        }
        else
        {
            return Direction.Invalid;
        }
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