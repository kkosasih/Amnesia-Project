using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int teamID;
    public bool onMap = false;
    public int currentTile;
    public int movementPreventions = 0;
    public float delay;
    public float lastMove = 0.0f;
    public int lastTile = -1;
    public int health;
    public int maxHealth;
    public GameObject healthSlider;
    protected bool attacked = false;
    protected Coroutine movementRoutine;
    protected Animator _animator;

    protected virtual void Awake ()
    {
        healthSlider = Instantiate((GameObject)Resources.Load("GUI/CharHealthSlider"), GameObject.Find("DynamicCanvas").transform);
        UITracking uit = healthSlider.GetComponent<UITracking>();
        uit.obj = gameObject;
        uit.offset = new Vector3(0, 40, 0);
    }

    // Use this for initialization
    protected virtual void Start ()
    {
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
            switch (path[i])
            {
                case Direction.Up:
                    Move(GameController.map.TileAbove(currentTile), path[i]);
                    break;
                case Direction.Down:
                    Move(GameController.map.TileBelow(currentTile), path[i]);
                    break;
                case Direction.Left:
                    Move(GameController.map.TileLeft(currentTile), path[i]);
                    break;
                case Direction.Right:
                    Move(GameController.map.TileRight(currentTile), path[i]);
                    break;
            }
            if (i < path.Count - 1)
            {
                yield return new WaitForSeconds(delay);
            }
        }
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
            transform.position = GameController.map.tiles[currentTile].transform.position + new Vector3(0, 0, -1);
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
        Vector3 oldPos = transform.position;
        Vector3 newPos = GameController.map.tiles[moveTo].transform.position + new Vector3(0, 0, -1);
        for (float timePassed = 0; timePassed < Mathf.Min(0.5f, delay); timePassed += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timePassed / Mathf.Min(0.5f, delay));
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        currentTile = moveTo;
        HandleTile();
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
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}